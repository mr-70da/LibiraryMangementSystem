using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authentications
{
    public class RegisterCommand : IRequest<GeneralResponse<LoginResponseDto>>
    {
        public RegisterRequestDto RegisterDto { get; }
        public RegisterCommand(RegisterRequestDto registerDto)
        {
            RegisterDto = registerDto;
        }
    }

  
}
