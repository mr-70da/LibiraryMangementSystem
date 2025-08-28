using LibraryManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
        Task<LoginResponseDto> Register(RegisterRequestDto requestDto);
    }
}
