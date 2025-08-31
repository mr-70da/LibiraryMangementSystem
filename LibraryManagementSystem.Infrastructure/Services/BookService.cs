using AutoMapper;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task CreateAsync(BookCreateDto bookDto)
        {
            var newBook = _mapper.Map<Book>(bookDto);
            var author = await _unitOfWork.Authors.GetByIdAsync(bookDto.AuthorId);
            if (author == null)
            {
                throw new Exception("Author not found");
            }
            await _unitOfWork.Books.AddAsync(newBook);
            await _unitOfWork.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            _unitOfWork.Books.Remove(book);
            await _unitOfWork.Complete();
        }

        public async Task<BooksFilterResponse> GetBooksAsync(BooksFilterRequest filter)
        {
            var query = await _unitOfWork.Books.GetFilteredBooksAsync(
                filter.AuthorId, filter.BookName, filter.BranchId);

            int totalBooks = query.Count();

            //mynf3sh ytfslo lw7do 3ashan kda 3mlt pagination 3la el query
            var paginatedBooks = query
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToList();

            var bookDto = _mapper.Map<List<BookReadDto>>(paginatedBooks);

           return new BooksFilterResponse
           {
               TotalCount = totalBooks,
               Books = bookDto
           };
        }
        /*create procedure SearchBookWithFilters
	@Title varchar(200) = NULL,
	@AuthorId int = NULL,
	@BranchId int = NULL
	as

  begin 
	SELECT *
	FROM Book
	WHERE (@Title is NULL or Title = @Title) 
		AND (@AuthorId is NULL or AuthorId = @AuthorId) 
		AND(@BranchId is NULL or BranchId = @BranchId)
	end

	exec SearchBookWithFilters @AuthorID = 1 , @BranchId = 1 ,@Title = '1984';
         */



        public async Task UpdateAsync(int id , BookCreateDto newBookDto)
        {
            if (await _unitOfWork.Books.GetByIdAsync(id) == null)
            {
                throw new KeyNotFoundException("Book not found");
            }
            var newBook = _mapper.Map<Book>(newBookDto);
            newBook.Isbn = id;
            _unitOfWork.Books.Update( newBook);
            await _unitOfWork.Complete(); 
        }

        public async Task<List<BooksPerBranchDto>> GetBooksCountPerBranchAsync()
        {
            var result = (from b in await _unitOfWork.Books.GetAllAsync()
                          join l in await _unitOfWork.Branches.GetAllAsync()
                              on b.BranchId equals l.Id
                          group b by new { b.BranchId, l.BranchName } into g
                          select new BooksPerBranchDto((int)g.Key.BranchId, g.Key.BranchName,  g.Count())
                          ).ToList();

            return result;
        }


        public async Task<List<MostBorrowedBooksDto>> GetMostBorrowedAsync(int listSize)
        {
            var result = (from bh in await _unitOfWork.Borrowings.GetAllAsync()
                          join b in await _unitOfWork.Books.GetAllAsync()
                              on bh.BookId equals b.Isbn
                          group bh by new { b.Isbn, b.Title } into g
                          orderby g.Count() descending
                          select new MostBorrowedBooksDto
                          {
                              BookId = g.Key.Isbn,
                              Title = g.Key.Title,
                              BorrowCount = g.Count()
                          })
                          .Take(listSize)
                          .ToList();

            return result;
        }
        public async Task UpdateStatusAsync(int bookIsbn , BookStatus newStatus)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookIsbn);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }
            book.Status = newStatus;
            _unitOfWork.Books.Update(book);
            
            await _unitOfWork.Complete();
        }

        public async Task ReturnAsync(int TransactionId)
        {
            var transaction = await _unitOfWork.Borrowings.GetByIdAsync(TransactionId);
            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found");
            }
            var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(TransactionId);
            if (borrowing.ReturnDate.HasValue)
            {
                throw new Exception("This Book is already returned.");
            }
            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            var book = await _unitOfWork.Books.GetByIdAsync((int)borrowing.BookId);
            book.Status = BookStatus.Available;
            _unitOfWork.Borrowings.Update(borrowing);
            
            await _unitOfWork.Complete();
        }
        //Ask
        internal bool CheckBookAvailability(Book book)
        {
            if (book.Status != BookStatus.Available)
            {
                throw new Exception("Book is not available");
            }
            return true;
        }
        public async Task BorrowAsync(int UserId, int BookIsbn)
        {
            if(UserId == null || BookIsbn == null){
                throw new ArgumentNullException("User id and book ISBN should be specified");

            }
            var user = await _unitOfWork.Users.GetByIdAsync(UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            var book = await _unitOfWork.Books.GetByIdAsync(BookIsbn);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }
            if (CheckBookAvailability(book))
            {
                
                BorrowingHistory borrowingHistory = new BorrowingHistory();
                book.Status = BookStatus.Borrowed;
                _unitOfWork.Books.Update(book);
                
                borrowingHistory.UserId = UserId;
                borrowingHistory.BookId = BookIsbn;
                borrowingHistory.BorrowDate =
                    DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.Borrowings.AddAsync(borrowingHistory);
                await _unitOfWork.Complete();
                
            }
            


        }
    }
}
