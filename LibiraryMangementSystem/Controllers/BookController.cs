using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] BookCreateDto newBook)
        {
            await _bookService.CreateAsync(newBook);
            return Ok("Created!");
        }

        //checked
        [HttpPut("{bookIsbn}")]
        [Authorize]
        public async Task<IActionResult> Update(int bookIsbn, [FromBody] BookCreateDto updatedBook)
        {
            
            await _bookService.UpdateAsync(bookIsbn, updatedBook);
            return Ok("Updated!");

        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int bookIsbn , BookStatus status)
        {
            
            await _bookService.UpdateStatusAsync(bookIsbn, status);
            return Ok("Book's status updated successfully.");
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Borrow(int UserId, int BookIsbn)
        {
            
            await _bookService.BorrowAsync(UserId, BookIsbn);
            return Ok("Borrowed!");
        }
        [HttpPut]
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
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetBooksCountPerBranch()
        {
            
            return Ok(await _bookService.GetBooksCountPerBranchAsync());
        }

    }
}
