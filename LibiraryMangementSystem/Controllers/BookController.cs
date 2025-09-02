
using LibraryManagementSystem.Application.Commands.Books;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        public BookController( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMostBorrowed(int listSize)
        {
            
            return Ok(await _mediator.Send(new GetMostBorrowedBooksQuery(listSize)));
        }
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetBooksCountPerBranch()
        {
            
            return Ok(await _mediator.Send(new GetBooksCountPerBranchQuery()));
        }
        
        [HttpGet]
        [Authorize]
        public async Task <IActionResult> All([FromQuery] GetAllBooksQuery filter)
        {
           
            return Ok(await _mediator.Send(filter));
            
        }

        //create new book with author
        //Checked
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand  newBook)
        {
            return Ok(await _mediator.Send(newBook));
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateBookCommand bookUpdate)

        {
            return Ok(await _mediator.Send(bookUpdate));

        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateStatus(UpdateBookStatusCommand updateDto)
        {            
            return Ok(await _mediator.Send(updateDto));
        }
        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Borrow(BorrowBookCommand borrowRequest)
        {
            return Ok(await _mediator.Send(borrowRequest));
        }
        [HttpPut]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Return(int TransactionId)
        {
            return Ok(await _mediator.Send(new ReturnBookCommand(TransactionId)));
        }

    }
}
