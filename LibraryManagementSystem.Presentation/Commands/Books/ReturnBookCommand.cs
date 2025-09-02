using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class ReturnBookCommand : IRequest<GeneralResponse<BookReadResponse>>
    {
        public int TransactionId { get; set; }

        public ReturnBookCommand(int transactionId)
        {
            TransactionId = transactionId;
        }
    }
}