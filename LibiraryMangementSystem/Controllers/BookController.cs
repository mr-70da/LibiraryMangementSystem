using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService service)
        {
            _bookService= service;
        }

        [HttpGet("byAuther/{authorId}")]
        public IActionResult GetAll(int authorId)
        {
            _bookService.GetAllByAuthor(authorId);
            return Ok();
        }
        //create new book with author
        [HttpPost("{authorId}")]
        public IActionResult Create(int authorId, [FromBody] BookCreateDto newBook)
        {
            _bookService.Create(authorId, newBook);
            return Ok("Created!");
        }
        [HttpPut("{bookIsbn}")]
        public IActionResult Update(int bookIsbn, [FromBody] BookCreateDto updatedBook)
        {
            _bookService.Update(bookIsbn, updatedBook);
            return Ok("Updated!");

        }

    }
}
