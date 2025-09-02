using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class UpdateBookStatusCommand : IRequest<GeneralResponse<BookReadResponse>>
    {
        public int BookIsbn { get; set; }
        public BookStatus BookStatus { get; set; }

        public UpdateBookStatusCommand(int bookIsbn, BookStatus bookStatus)
        {
            BookIsbn = bookIsbn;
            BookStatus = bookStatus;
        }
    }
}