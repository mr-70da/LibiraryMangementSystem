using System.Security.Claims;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService service)
        {
            _bookService = service;
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
            await _bookService.CreateAsync(newBook);
            return Ok("Created!");
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(BookUpdateRequestDto bookUpdate)

        {
            
            await _bookService.UpdateAsync(bookUpdate);
            return Ok("Updated!");

        }
        public async Task<IActionResult> UpdateStatus(BookStatusUpdateDto updateDto)
        {
            
            await _bookService.UpdateStatusAsync(updateDto);
            return Ok("Book's status updated successfully.");
        }
        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Borrow(BorrowRequestDto borrowRequest)
        {
            //add user id from token to borrow request
            await _bookService.BorrowAsync(borrowRequest);
            return Ok("Borrowed!");
        }
        [HttpPut]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Return(int TransactionId)
        {
            
            await _bookService.ReturnAsync(TransactionId);
            return Ok("Returned!");
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
