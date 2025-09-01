using System.Security.Claims;
using LibraryManagementSystem.API.Queries;
using LibraryManagementSystem.Application.Commands.Users;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            return Ok(await _mediator.Send(new CreateUserCommand(user)));
        }
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> BorrowingHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetUserBorrowingHistoryQuery(int.Parse(userId));
            return Ok(_mediator.Send(query));
        }
    }
}
