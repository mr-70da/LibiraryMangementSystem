using AutoMapper;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Application.Services.Interface;
using Azure;

namespace LibraryManagementSystem.Application.Services.Implementations
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


        public async Task<GeneralResponse<BookReadDto>> CreateAsync(BookCreateDto bookDto)
        {
            try
            {
                GeneralResponse<BookReadDto> response;
                var newBook = _mapper.Map<Book>(bookDto);
                var author = await _unitOfWork.Authors.GetByIdAsync(bookDto.AuthorId);
                if (author == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "Author not found", System.Net.HttpStatusCode.BadRequest);
                    return response;
                }
                await _unitOfWork.Books.AddAsync(newBook);
                await _unitOfWork.Complete();
                response = new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(newBook), true,
                    "Book created successfully", System.Net.HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the book: " + ex.Message);
            }
        }
        public async Task<GeneralResponse<BookReadDto>> DeleteAsync(int id)
        {
            try
            {
                GeneralResponse<BookReadDto> response;
                var book = await _unitOfWork.Books.GetByIdAsync(id);
                if (book == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                    throw new Exception("Book not found");
                }
                _unitOfWork.Books.Remove(book);
                await _unitOfWork.Complete();
                response = new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(book), true,
                    "Book deleted successfully", System.Net.HttpStatusCode.OK);
                return response;
            }
            catch
            (Exception ex)
            {
                throw new Exception("An error occurred while deleting the book: " + ex.Message);
            }

        }

        public async Task<GeneralResponse<BooksFilterResponse>> GetBooksAsync(BooksFilterRequest filter)
        {
            try
            {
                GeneralResponse<BooksFilterResponse> response;

                var query = await _unitOfWork.Books.GetFilteredBooksAsync(
              filter.AuthorId, filter.BookName, filter.BranchId);


                int totalBooks = query.Count();

                //mynf3sh ytfslo lw7do 3ashan kda 3mlt pagination 3la el query
                var paginatedBooks = query
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToList();

                var bookDto = _mapper.Map<List<BookReadDto>>(paginatedBooks);

                response = new GeneralResponse<BooksFilterResponse>
                    (new BooksFilterResponse
                    {
                        TotalCount = totalBooks,
                        Books = bookDto
                    },
                    true, "Books retrieved successfully", System.Net.HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving books: " + ex.Message);
            }
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



        public async Task<GeneralResponse<BookReadDto>> UpdateAsync(BookUpdateRequestDto newBookDto)
        {
            try
            {
                GeneralResponse<BookReadDto> response;

                if (await _unitOfWork.Books.GetByIdAsync(newBookDto.Isbn) == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);

                    return response;

                }
                var newBook = _mapper.Map<Book>(newBookDto);
                _unitOfWork.Books.Update(newBook);
                await _unitOfWork.Complete();
                response = new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(newBook), true,
                    "Book updated successfully", System.Net.HttpStatusCode.OK);
                return response;

            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while updating the book: " + ex.Message);
            }
        }

        public async Task<GeneralResponse<List<BooksPerBranchDto>>> GetBooksCountPerBranchAsync()
        {
            try { 
                GeneralResponse<List<BooksPerBranchDto>> response;
                var result = (from b in await _unitOfWork.Books.GetAllAsync()
                               join l in await _unitOfWork.Branches.GetAllAsync()
                                   on b.BranchId equals l.Id
                               group b by new { b.BranchId, l.BranchName } into g
                               select new BooksPerBranchDto((int)g.Key.BranchId, g.Key.BranchName, g.Count())
                          ).ToList();
                
                response = new GeneralResponse<List<BooksPerBranchDto>>
                    (result, true, "Books count per branch retrieved successfully", System.Net.HttpStatusCode.OK);
                return response;

            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while retrieving books count per branch: " + ex.Message);
            }
           
        }


        public async Task<GeneralResponse<List<MostBorrowedBooksDto>>> GetMostBorrowedAsync(int listSize)
        {
            try
            {
                GeneralResponse<List<MostBorrowedBooksDto>> response;
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
                response = new GeneralResponse<List<MostBorrowedBooksDto>>
                    (result, true, "Most borrowed books retrieved successfully", System.Net.HttpStatusCode.OK);
                return response;
            }catch(Exception ex)
            {
                throw new Exception("An error occurred while retrieving most borrowed books: " + ex.Message);
            }
        }
        public async Task<GeneralResponse<BookReadDto>> UpdateStatusAsync(BookStatusUpdateDto updateDto)
        {
            try
            {
                GeneralResponse<BookReadDto> response;
                var book = await _unitOfWork.Books.GetByIdAsync(updateDto.BookIsbn);
                if (book == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                    return response;
                }
                book.Status = updateDto.BookStatus;
                _unitOfWork.Books.Update(book);

                await _unitOfWork.Complete();
                response = new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(book), true,
                    "Book status updated successfully", System.Net.HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the book status: " + ex.Message);
            }
        }

        public async Task<GeneralResponse<BookReadDto>> ReturnAsync(int TransactionId)
        {
            try
            {
                GeneralResponse<BookReadDto> response;

                var transaction = await _unitOfWork.Borrowings.GetByIdAsync(TransactionId);
                if (transaction == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "Transaction not found", System.Net.HttpStatusCode.NotFound);
                    return response;

                }
                var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(TransactionId);
                if (borrowing.ReturnDate.HasValue)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "This Book is already returned.", System.Net.HttpStatusCode.BadRequest);
                    return response;
                }
                borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                var book = await _unitOfWork.Books.GetByIdAsync((int)borrowing.BookId);
                book.Status = BookStatus.Available;
                _unitOfWork.Borrowings.Update(borrowing);

                await _unitOfWork.Complete();
                response = new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(book), true,
                    "Book returned successfully", System.Net.HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while returning the book: " + ex.Message);
            }
        }
        //Ask
        internal bool CheckBookAvailability(Book book)
        {
            if (book.Status != BookStatus.Available)
            {
                return false;
            }
            return true;
        }
        public async Task<GeneralResponse<BookReadDto>> BorrowAsync(BorrowRequestDto requestDto)
        {
            try
            {
                GeneralResponse<BookReadDto> response;
                if (requestDto.UserId == null || requestDto.BookIsbn == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "User id and book ISBN should be specified",
                        System.Net.HttpStatusCode.BadRequest);
                }
                var user = await _unitOfWork.Users.GetByIdAsync(requestDto.UserId);
                if (user == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "User not found", System.Net.HttpStatusCode.NotFound);

                }
                var book = await _unitOfWork.Books.GetByIdAsync(requestDto.BookIsbn);
                if (book == null)
                {
                    response = new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }
                if (CheckBookAvailability(book))
                {

                    BorrowingHistory borrowingHistory = new BorrowingHistory();
                    book.Status = BookStatus.Borrowed;
                    _unitOfWork.Books.Update(book);

                    borrowingHistory.UserId = requestDto.UserId;
                    borrowingHistory.BookId = requestDto.BookIsbn;
                    borrowingHistory.BorrowDate =
                        DateOnly.FromDateTime(DateTime.Now);
                    await _unitOfWork.Borrowings.AddAsync(borrowingHistory);
                    await _unitOfWork.Complete();
                    response = new GeneralResponse<BookReadDto>
                        (_mapper.Map<BookReadDto>(book), true,
                        "Book borrowed successfully", System.Net.HttpStatusCode.OK);
                }
                else
                {
                    response = new GeneralResponse<BookReadDto>
                              (null, false, "Book is not available", System.Net.HttpStatusCode.BadRequest);
                }
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while borrowing the book: " + ex.Message);
            }




        }
    }
}
