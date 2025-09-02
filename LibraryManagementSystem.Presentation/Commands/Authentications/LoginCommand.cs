using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authentications
{
    public class LoginCommand : IRequest<GeneralResponse<LoginResponse>>
    {
        
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public LoginCommand(String email , String password)
        {
            Email = email;
            Password = password;
        }
    }
}
