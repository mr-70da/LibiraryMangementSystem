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


        public void Create(BookCreateDto bookDto)
        {
            var newBook = _mapper.Map<Book>(bookDto);
            var author = _unitOfWork.Authors.GetById(bookDto.AuthorId);
            if (author == null)
            {
                throw new Exception("Author not found");
            }
            _unitOfWork.Books.Add(newBook);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var book = _unitOfWork.Books.GetById(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            _unitOfWork.Books.Remove(book);
            _unitOfWork.Complete();
        }

        public BooksByAuthorDto GetBooks(BookFilterDto filter)
        {
            var query = _unitOfWork.Books.GetFilteredBooks(filter.AuthorId, filter.BookName, filter.BranchId);

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



        public void Update(int id , BookCreateDto newBookDto)
        {
            if (_unitOfWork.Books.GetById(id) == null)
            {
                throw new Exception("Book not found");
            }
            var newBook = _mapper.Map<Book>(newBookDto);
            newBook.Isbn = id;
            _unitOfWork.Books.Update( newBook);
            _unitOfWork.Complete(); 
        }

        public void UpdateStatus(int bookIsbn , BookStatus newStatus)
        {
            var book = _unitOfWork.Books.GetById(bookIsbn);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            book.status = newStatus;
            _unitOfWork.Books.Update(book);
            _unitOfWork.Complete();
        }
        public List<BooksPerBranchDto> GetBooksCountPerBranch()
        {
            var result = (from b in _unitOfWork.Books.GetAll()
                          join l in _unitOfWork.Branches.GetAll()
                              on b.BranchId equals l.Id
                          group b by new { b.BranchId, l.BranchName } into g
                          select new BooksPerBranchDto((int)g.Key.BranchId, g.Key.BranchName,  g.Count())
                          ).ToList();

            return result;
        }


        public List<MostBorrowedBooksDto> GetMostBorrowed(int listSize)
        {
            var result = (from bh in _unitOfWork.Borrowings.GetAll()
                          join b in _unitOfWork.Books.GetAll()
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

        public void Return(int TransactionId)
        {
            var transaction = _unitOfWork.Borrowings.GetById(TransactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }
            var borrowing = _unitOfWork.Borrowings.GetById(TransactionId);
            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            var book = _unitOfWork.Books.GetById((int)borrowing.BookId);
            book.status = BookStatus.Available;
            _unitOfWork.Borrowings.Update(borrowing);
            _unitOfWork.Complete();
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
        public void Borrow(int UserId, int BookIsbn)
        {
            var user = _unitOfWork.Users.GetById(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var book = _unitOfWork.Books.GetById(BookIsbn);
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
                _unitOfWork.Borrowings.Add(borrowingHistory);
                _unitOfWork.Complete();
                
            }
            

        }
    }
}
