using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Authors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand newAuthor)
        {

            return Ok(await _mediator.Send(newAuthor));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int authorId)
        {            
            return Ok(await _mediator.Send(new DeleteAuthorCommand(authorId)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuthorCommand requestDto)
        {
             return Ok(await _mediator.Send(requestDto));
        }
    }
}
