using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class UpdateBookStatusCommand : IRequest<GeneralResponse<BookReadDto>>
    {
        public BookStatusUpdateDto UpdateDto { get; set; }

        public UpdateBookStatusCommand(BookStatusUpdateDto updateDto)
        {
            UpdateDto = updateDto;
        }
    }
}