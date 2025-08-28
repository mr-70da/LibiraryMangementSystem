using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
          
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var response = await _authenticationService.Login(request);
            if (response == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(response);
           
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Register(RegisterRequestDto request)
        {

            
            return Ok(await _authenticationService.Register(request));
        }

    }
}
