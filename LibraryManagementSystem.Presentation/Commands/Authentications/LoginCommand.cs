using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authentications
{
    public class LoginCommand : IRequest<GeneralResponse<LoginResponseDto>>
    {
        public LoginRequestDto LoginDto { get; }
        public LoginCommand(LoginRequestDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
