using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class DeleteBookCommand : IRequest<GeneralResponse<BookReadDto>>
    {
        public int Id { get; set; }

        public DeleteBookCommand(int id)
        {
            Id = id;
        }
    }
}