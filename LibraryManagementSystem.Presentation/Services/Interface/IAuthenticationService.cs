using LibraryManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<GeneralResponse<LoginResponseDto>> Login(LoginRequestDto requestDto);
        Task<GeneralResponse<LoginResponseDto>> Register(RegisterRequestDto requestDto);
    }
}
