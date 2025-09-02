using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<GeneralResponse<BookReadResponse>>
    {
        public int Isbn { get; set; }
        public int AuthorId { get; set; }

        public UpdateBookCommand(int isbn, int authorId)
        {
            Isbn = isbn;
            AuthorId = authorId;
        }
    }
}