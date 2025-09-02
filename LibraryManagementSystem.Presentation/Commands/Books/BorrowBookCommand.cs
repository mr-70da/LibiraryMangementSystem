using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class BorrowBookCommand : IRequest<GeneralResponse<BookReadResponse>>
    {
        
        public int UserId { get; set; }
        public int BookIsbn { get; set; }

        public BorrowBookCommand(int UserId , int BookIsbn)
        {
            this.BookIsbn = BookIsbn;
            this.UserId = UserId;
        }
    }
}