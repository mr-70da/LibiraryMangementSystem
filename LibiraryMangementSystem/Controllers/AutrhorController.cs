using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutrhorController : ControllerBase
    {
        private LibraryContext _libraryContext;
        public AutrhorController(LibraryContext context)
        {
            _libraryContext = context;
        }

        //Create new auther
        [HttpPost()]
        public IActionResult Create([FromBody] Author newAuthor)
        {
            if (newAuthor == null)
            {
                return BadRequest("Invalid author data.");
            }
            if (_libraryContext.Authors.Any(a => a.FirstName == newAuthor.FirstName && a.LastName == newAuthor.LastName))
            {
                return Conflict("Author already exists.");
            }
            _libraryContext.Authors.Add(newAuthor);
            _libraryContext.SaveChanges();
            return Ok("Created!");
        }
        [HttpDelete()]
        public IActionResult Delete(int authorId)
        {
            var author = _libraryContext.Authors.Find(authorId);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
            if (_libraryContext.Books.Any(aob => aob.Authors.Any(a => a.Id == authorId)))
            {
                return BadRequest("Cannot delete author with associated books.");
            }
            _libraryContext.Authors.Remove(author);
            _libraryContext.SaveChanges();
            return Ok("Author deleted successfully.");
        }

        [HttpPut()]
        public IActionResult Update(int authorId, [FromBody] Author updatedAuthor)
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
            existingAuthor.FirstName = updatedAuthor.FirstName;
            existingAuthor.LastName = updatedAuthor.LastName;
            _libraryContext.SaveChanges();
            return Ok(existingAuthor);
        }
    }
}
