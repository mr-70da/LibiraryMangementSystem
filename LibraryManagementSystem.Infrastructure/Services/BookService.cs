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

        public async Task<BooksByAuthorDto> GetBooksAsync(BookFilterDto filter)
        {
            var query = await _unitOfWork.Books.GetFilteredBooksAsync(filter.AuthorId, filter.BookName, filter.BranchId);

            int totalBooks = query.Count();
            int totalPages = (int)Math.Ceiling(totalBooks / (double)filter.PageSize);

            var paginatedBooks = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var bookDto = _mapper.Map<List<BookWithoutAuthorDto>>(paginatedBooks);

            return new BooksByAuthorDto(
                filter.AuthorId ?? 0,
                bookDto,
                totalBooks
                
            );
        }



        public async Task UpdateAsync(int id , BookCreateDto newBookDto)
        {
            if (await _unitOfWork.Books.GetByIdAsync(id) == null)
            {
                throw new Exception("Book not found");
            }
            var newBook = _mapper.Map<Book>(newBookDto);
            newBook.Isbn = id;
            _unitOfWork.Books.Update( newBook);
            await _unitOfWork.Complete(); 
        }

        public async Task UpdateStatusAsync(int bookIsbn , BookStatus newStatus)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookIsbn);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            book.status = newStatus;
            _unitOfWork.Books.Update(book);
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

        public async Task ReturnAsync(int TransactionId)
        {
            var transaction = await _unitOfWork.Borrowings.GetByIdAsync(TransactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }
            var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(TransactionId);
            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            var book = await _unitOfWork.Books.GetByIdAsync((int)borrowing.BookId);
            book.status = BookStatus.Available;
            _unitOfWork.Borrowings.Update(borrowing);
            await _unitOfWork.Complete();
        }
        //Ask
        internal bool CheckBookAvailability(Book book)
        {
            if (book.status != BookStatus.Available)
            {
                throw new Exception("Book is not available");
            }
            return true;
        }
        public async Task BorrowAsync(int UserId, int BookIsbn)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var book = await _unitOfWork.Books.GetByIdAsync(BookIsbn);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            if (CheckBookAvailability(book))
            {   
                BorrowingHistory borrowingHistory = new BorrowingHistory();
                book.status = BookStatus.Borrowed;
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
