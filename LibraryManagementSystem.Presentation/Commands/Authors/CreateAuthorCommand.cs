using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authors
{
    public class CreateAuthorCommand : IRequest<GeneralResponse<AuthorReadResponse>>
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public CreateAuthorCommand(String FirstName, String LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        
    }
}
