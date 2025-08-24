using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Application.DTOs;

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

        [HttpGet("byAuther/{authorId}")]
        public IActionResult All(int authorId)
        {
            return Ok(_bookService.GetAllByAuthor(authorId));

        }
        //create new book with author
        //Checked
        [HttpPost]
        public IActionResult Create([FromBody] BookCreateDto newBook)
        {
            _bookService.Create(newBook);
            return Ok("Created!");
        }
        //checked
        [HttpPut("{bookIsbn}")]
        public IActionResult Update(int bookIsbn, [FromBody] BookCreateDto updatedBook)
        {
            _bookService.Update(bookIsbn, updatedBook);
            return Ok("Updated!");

        }
        [HttpPut]
        public IActionResult UpdateStatus(int bookIsbn)
        {

            return Ok("Book deleted successfully.");
        }
        [HttpPut]
        public IActionResult Borrow(int UserId, int BookIsbn, int BranchId)
        {

            return Ok();
        }
        [HttpPut]
        public IActionResult Return(int TransactionId)
        {

            return Ok();
        }
        [HttpGet]
        public IActionResult GetMostBorrowed(int listSize)
        {
            // Logic to get borrowing history by user ID
            return Ok("Borrowing history retrieved successfully.");
        }
        [HttpGet]
        public IActionResult GetBooksCountPerBranch()
        {
            // Logic to get borrowing history by user ID
            return Ok("Borrowing history retrieved successfully.");
        }

    }
}
