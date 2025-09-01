using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authors
{
    public class CreateAuthorCommand : IRequest<GeneralResponse<AuthorReadDto>>
    {
        public AuthorCreateDto AuthorDto { get; set; }
        public CreateAuthorCommand(AuthorCreateDto authorDto)
        {
            AuthorDto = authorDto;
        }
        
    }
}
