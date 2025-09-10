using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class BorrowBookHandler : IRequestHandler<BorrowBookCommand, GeneralResponse<BookReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BorrowBookHandler> _logger;

        public BorrowBookHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<BorrowBookHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<BookReadResponse>> 
            Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId == null || request.BookIsbn == null)
                {
                    _logger.LogWarning("Borrow book request missing UserId or BookIsbn.");
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "User id and book ISBN should be specified",
                        System.Net.HttpStatusCode.BadRequest);
                }

                var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    _logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "User not found", System.Net.HttpStatusCode.NotFound);
                }

                var book = await _unitOfWork.Books.GetByIdAsync(request.BookIsbn);
                if (book == null)
                {
                    _logger.LogWarning("Book not found with ISBN: {BookIsbn}", request.BookIsbn);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }

                if (!IsBookAvailable(book))
                {
                    _logger.LogInformation("Book with ISBN: {BookIsbn} is not available for borrowing.", request.BookIsbn);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "Book is not available", System.Net.HttpStatusCode.BadRequest);
                }

                var borrowingHistory = new BorrowingHistory
                {
                    UserId = request.UserId,
                    BookId = request.BookIsbn,
                    BorrowDate = DateOnly.FromDateTime(DateTime.Now)
                };

                book.Status = BookStatus.Borrowed;
                _unitOfWork.Books.Update(book);
                await _unitOfWork.Borrowings.AddAsync(borrowingHistory);
                await _unitOfWork.Complete();
                _logger.LogInformation("Book with ISBN: {BookIsbn} borrowed successfully by User ID: {UserId}.",
                    request.BookIsbn, request.UserId);

                return new GeneralResponse<BookReadResponse>
                    (_mapper.Map<BookReadResponse>(book), true,
                    "Book borrowed successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        private bool IsBookAvailable(Book book)
        {
            _logger.LogDebug("Checking availability for book with ISBN: {BookIsbn}, Current Status: {BookStatus}",
                book.Isbn, book.Status);
            return book.Status == BookStatus.Available;
        }
    }
}
