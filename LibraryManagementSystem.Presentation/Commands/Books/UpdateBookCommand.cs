using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<GeneralResponse<BookReadDto>>
    {
        public BookUpdateRequestDto BookDto { get; set; }

        public UpdateBookCommand(BookUpdateRequestDto bookDto)
        {
            BookDto = bookDto;
        }
    }
}