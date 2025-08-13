
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryManagementSystem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private LibiraryContext _libraryContext;
        public LibraryController(LibiraryContext context)
        {
            _libraryContext = context;
        }
        [HttpPost("CreateNewAuthor")]
        public IActionResult CreateNewAuthor([FromBody] Author newAuthor)
        {
            if(newAuthor == null)
            {
                return BadRequest("Invalid author data.");
            }
            if(_libraryContext.Authors.Any(a => a.AuthorFname == newAuthor.AuthorFname && a.AuthorLname == newAuthor.AuthorLname))
            {
                return Conflict("Author already exists.");
            }
            _libraryContext.Authors.Add(newAuthor);
            _libraryContext.SaveChanges();
            return Ok("Created!");
        }
        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(int bookIsbn,[FromBody] Book updatedBook)
        {
            if(updatedBook == null)
            {
                return BadRequest("Invalid Book data.");
            }
            Book existingBook = _libraryContext.Books.Find(bookIsbn);
            if(existingBook == null)
            {
                return BadRequest("Book not found!");
            }
            existingBook.BookTitle = updatedBook.BookTitle;
            existingBook.Price = updatedBook.Price;
            existingBook.BookCopyRight = updatedBook.BookCopyRight;
            existingBook.BookEdition = updatedBook.BookEdition;
            _libraryContext.SaveChanges();
            return Ok("Updated!");

        }

        [HttpPut("UpdateAuthor")]
        public IActionResult UpdateAuthor(int authorId,[FromBody] Author updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                return BadRequest("Invalid author data.");
            }
            var existingAuthor = _libraryContext.Authors.Find(authorId);
            if (existingAuthor == null)
            {
                return NotFound("Author not found.");
            }
            existingAuthor.AuthorFname = updatedAuthor.AuthorFname;
            existingAuthor.AuthorLname = updatedAuthor.AuthorLname;
            _libraryContext.SaveChanges();
            return Ok(existingAuthor);
        }

        [HttpDelete("DeleteAuthor")]
        public IActionResult DeleteAuthor(int authorId)
        { 
            var author = _libraryContext.Authors.Find(authorId);
            if (author == null)
            {
                return NotFound("Author not found.");
            } 
            if (_libraryContext.Books.Any(aob => aob.Authors.Any(a=>a.AuthorId== authorId)))
            {
                return BadRequest("Cannot delete author with associated books.");
            }
            _libraryContext.Authors.Remove(author);
            _libraryContext.SaveChanges();
            return Ok("Author deleted successfully.");
        }

        //create new book with author
        [HttpPost("CreateNewBook")]
        public IActionResult CreateNewBook(int authorId, [FromBody] Book newBook)
        {
            if (newBook == null)
                return BadRequest("Invalid book data.");


            Author author = _libraryContext.Authors.Find(authorId);

            if (author == null)
                return BadRequest("Author not found.");

            author.BookIsbns.Add(newBook);

            _libraryContext.SaveChanges();

            return Ok("Created!");
        }

        [HttpGet("GetAllBooksByAuthor/{author_id}")]
        public IActionResult getAllBooksByAuthor(int author_id)
        {
            var booksByAuthor = _libraryContext.Books
                               .Where(b => b.Authors.Any(a => a.AuthorId == author_id))
                               .Select(b => new
                               {    
                                   b.BookIsbn,
                                   b.BookTitle,
                                   b.BookEdition,
                                   b.Price,
                                  
                               })
                               .ToList();

            var result = new
            {
                AuthorId = author_id,
                TotalBooks = booksByAuthor.Count,
                Books = booksByAuthor
            };

            return Ok(result);
        }

    }
}
