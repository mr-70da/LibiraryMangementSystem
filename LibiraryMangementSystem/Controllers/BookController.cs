using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;

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
        public IActionResult All([FromQuery] BookFilterDto filter)
        {
            try
            {
                var books = _bookService.GetBooks(filter);
                return Ok(books);
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }

        //create new book with author
        //Checked
        [HttpPost]
        public IActionResult Create([FromBody] BookCreateDto newBook)
        {
            try
            {
                _bookService.Create(newBook);
            }
            catch (Exception e)
            {
                string message = e.Message;
                return StatusCode(404, message);
            }
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
        public IActionResult UpdateStatus(int bookIsbn , BookStatus status)
        {
            
            _bookService.UpdateStatus(bookIsbn, status);
            return Ok("Book's status updated successfully.");
        }
        [HttpPut]
        public IActionResult Borrow(int UserId, int BookIsbn)
        {
            
            _bookService.Borrow(UserId, BookIsbn);
            return Ok("Borrowed!");
        }
        [HttpPut]
        public IActionResult Return(int TransactionId)
        {
            
            _bookService.Return(TransactionId);
            return Ok("Returned!");
        }
        [HttpGet]
        public IActionResult GetMostBorrowed(int listSize)
        {
            
            return Ok(_bookService.GetMostBorrowed(listSize));
        }
        [HttpGet]
        public IActionResult GetBooksCountPerBranch()
        {
            
            return Ok(_bookService.GetBooksCountPerBranch());
        }

    }
}
