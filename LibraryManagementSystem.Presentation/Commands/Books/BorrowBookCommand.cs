using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class BorrowBookCommand : IRequest<GeneralResponse<BookReadDto>>
    {
        public BorrowRequestDto RequestDto { get; set; }

        public BorrowBookCommand(BorrowRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}