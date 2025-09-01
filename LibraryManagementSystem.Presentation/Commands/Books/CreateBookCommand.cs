using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<GeneralResponse<BookReadDto>>
    {
        public BookCreateDto BookDto { get; set; }

        public CreateBookCommand(BookCreateDto bookDto)
        {
            BookDto = bookDto;
        }
    }
}