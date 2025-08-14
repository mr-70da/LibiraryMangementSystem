using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        LibraryContext _libraryContext;
        public BookController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet("byAuther/{authorId}")]
        public IActionResult GetAll(int authorId)
        {
            var booksByAuthor = _libraryContext.Books
                               .Where(b => b.Authors.Any(a => a.Id == authorId))
                               .Select(b => new
                               {
                                   b.Isbn,
                                   b.Title,
                                   b.Edition,
                                   b.Price,

                               })
                               .ToList();

            var result = new
            {
                AuthorId = authorId,
                TotalBooks = booksByAuthor.Count,
                Books = booksByAuthor
            };

            return Ok(result);
        }
        //create new book with author
        [HttpPost("{authorId}")]
        public IActionResult Create(int authorId, [FromBody] Book newBook)
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
        [HttpPut("{bookIsbn}")]
        public IActionResult Update(int bookIsbn, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("Invalid Book data.");
            }
            Book existingBook = _libraryContext.Books.Find(bookIsbn);
            if (existingBook == null)
            {
                return BadRequest("Book not found!");
            }
            existingBook.Title = updatedBook.Title;
            existingBook.Price = updatedBook.Price;
            existingBook.CopyRightYear = updatedBook.CopyRightYear;
            existingBook.Edition = updatedBook.Edition;
            _libraryContext.SaveChanges();
            return Ok("Updated!");

        }

    }
}
