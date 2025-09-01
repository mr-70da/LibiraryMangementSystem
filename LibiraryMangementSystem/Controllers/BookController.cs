using System.Security.Claims;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMediator _mediator;
        public BookController(IBookService service , IMediator mediator)
        {
            _bookService = service;
            _mediator = mediator;
        }

        
        [HttpGet]
        [Authorize]
        public async Task <IActionResult> All([FromQuery] BooksFilterRequest filter)
        {
            var books = await _bookService.GetBooksAsync(filter);
            return Ok(books);
            
        }

        //create new book with author
        //Checked
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] BookCreateDto newBook)
        {
            return Ok(await _bookService.CreateAsync(newBook));
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(BookUpdateRequestDto bookUpdate)

        {
            return Ok(await _bookService.UpdateAsync(bookUpdate));

        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateStatus(BookStatusUpdateDto updateDto)
        {            
            return Ok(await _bookService.UpdateStatusAsync(updateDto));
        }
        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Borrow(BorrowRequestDto borrowRequest)
        {
            return Ok(await _bookService.BorrowAsync(borrowRequest));
        }
        [HttpPut]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Return(int TransactionId)
        {
            return Ok(await _bookService.ReturnAsync(TransactionId));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMostBorrowed(int listSize)
        {
            
            return Ok(await _bookService.GetMostBorrowedAsync(listSize));
        }
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetBooksCountPerBranch()
        {
            
            return Ok(await _bookService.GetBooksCountPerBranchAsync());
        }

    }
}
