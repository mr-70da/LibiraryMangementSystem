
using LibraryManagementSystem.Application.Commands.Authentications;
using LibraryManagementSystem.Application.DTOs;

using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
            
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {

            var response = await _mediator.Send(new LoginCommand(request));
            return Ok(response);
           
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Register(RegisterRequestDto request)
        {

            var response = await _mediator.Send(new RegisterCommand(request));
            return Ok(request);
        }

    }
}
