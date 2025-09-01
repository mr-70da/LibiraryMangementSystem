using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class ReturnBookHandler : IRequestHandler<ReturnBookCommand, GeneralResponse<BookReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReturnBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<BookReadDto>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = await _unitOfWork.Borrowings.GetByIdAsync(request.TransactionId);
                if (transaction == null)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "Transaction not found", System.Net.HttpStatusCode.NotFound);
                }

                if (transaction.ReturnDate.HasValue)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "This Book is already returned.", System.Net.HttpStatusCode.BadRequest);
                }

                transaction.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                var book = await _unitOfWork.Books.GetByIdAsync((int)transaction.BookId);
                book.Status = BookStatus.Available;

                _unitOfWork.Borrowings.Update(transaction);
                await _unitOfWork.Complete();

                return new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(book), true,
                    "Book returned successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while returning the book: " + ex.Message);
            }
        }
    }
}
