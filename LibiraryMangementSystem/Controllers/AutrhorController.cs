using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Interfaces.Services;
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
            try
            {
                _authorService.Create(newAuthor);
            }
            catch (Exception e)
            {
                string message = e.Message;
                return StatusCode(404,message);

            }
           
            return Ok("Created!");
        }
        [HttpDelete()]
        public IActionResult Delete(int authorId)
        {
            try
            {
                _authorService.Delete(authorId);
            }
            catch (Exception e)
            {
                string message = e.Message;
                return StatusCode(404,message);
            }
            return Ok("Author deleted successfully.");
        }

        [HttpPut()]
        public IActionResult Update(int authorId, [FromBody] AuthorCreateDto updatedAuthor)
        {
            try
            {
                _authorService.Update(authorId, updatedAuthor);
            }
            catch (Exception e)
            {
                string message = e.Message;
                return StatusCode(404,message);
            }
            
            return Ok("Updated");
        }
    }
}
