
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
        public async Task <IActionResult> All([FromQuery] BooksFilterRequest filter)
        {
           
            return Ok(await _mediator.Send(new GetAllBooksQuery(filter)));
            
        }

        //create new book with author
        //Checked
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] BookCreateDto newBook)
        {
            return Ok(await _mediator.Send(new CreateBookCommand(newBook)));
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(BookUpdateRequestDto bookUpdate)

        {
            return Ok(await _mediator.Send(new UpdateBookCommand(bookUpdate)));

        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateStatus(BookStatusUpdateDto updateDto)
        {            
            return Ok(await _mediator.Send(new UpdateBookStatusCommand(updateDto)));
        }
        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Borrow(BorrowRequestDto borrowRequest)
        {
            return Ok(await _mediator.Send(new BorrowBookCommand(borrowRequest)));
        }
        [HttpPut]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Return(int TransactionId)
        {
            return Ok(await _mediator.Send(new ReturnBookCommand(TransactionId)));
        }

    }
}
