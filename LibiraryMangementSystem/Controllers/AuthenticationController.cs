using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;
        public AuthenticationController(IAuthenticationService authenticationService ,IMediator mediator)
        {
            _mediator = mediator;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {

            var response = await _authenticationService.Login(request);
            return Ok(response);
           
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Register(RegisterRequestDto request)
        {

            
            return Ok(await _authenticationService.Register(request));
        }

    }
}
