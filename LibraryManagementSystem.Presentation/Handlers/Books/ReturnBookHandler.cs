using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class ReturnBookHandler : IRequestHandler<ReturnBookCommand, GeneralResponse<BookReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ReturnBookHandler> _logger;

        public ReturnBookHandler(IUnitOfWork unitOfWork, IMapper mapper ,ILogger<ReturnBookHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<BookReadResponse>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = await _unitOfWork.Borrowings.GetByIdAsync(request.TransactionId);
                if (transaction == null)
                {
                    _logger.LogWarning("Transaction not found with ID: {TransactionId}", request.TransactionId);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "Transaction not found", System.Net.HttpStatusCode.NotFound);
                }

                if (transaction.ReturnDate.HasValue)
                {
                    _logger.LogInformation("Attempted to return a book for transaction ID: {TransactionId} which is already returned.", request.TransactionId);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "This Book is already returned.", System.Net.HttpStatusCode.BadRequest);
                }

                transaction.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                var book = await _unitOfWork.Books.GetByIdAsync((int)transaction.BookId);
                book.Status = BookStatus.Available;

                _unitOfWork.Borrowings.Update(transaction);
                await _unitOfWork.Complete();
                _logger.LogInformation("Book returned successfully for transaction ID: {TransactionId}", request.TransactionId);

                return new GeneralResponse<BookReadResponse     >
                    (_mapper.Map<BookReadResponse>(book), true,
                    "Book returned successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning the book for transaction ID: {TransactionId}", request.TransactionId);
                throw new Exception("An error occurred while returning the book: " + ex.Message);
            }
        }
    }
}
