using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Author;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        //Create new auther
        [HttpPost()]
        public IActionResult Create([FromBody] AuthorCreateDto newAuthor)
        {
            _authorService.Create(newAuthor);
            return Ok("Created!");
        }
        [HttpDelete()]
        public IActionResult Delete(int authorId)
        {
            _authorService.Delete(authorId);
            return Ok("Author deleted successfully.");
        }

        [HttpPut()]
        public IActionResult Update(int authorId, [FromBody] AuthorCreateDto updatedAuthor)
        {
            _authorService.Update(authorId, updatedAuthor);
            return Ok("Updated");
        }
    }
}
