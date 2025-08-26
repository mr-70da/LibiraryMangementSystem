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
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto newAuthor)
        {
            await _authorService.CreateAsync(newAuthor);
            return Ok("Created!");
        }
        [HttpDelete()]
        public async Task<IActionResult> Delete(int authorId)
        {
            
            await _authorService.DeleteAsync(authorId);
       
            return Ok("Author deleted successfully.");
        }

        [HttpPut()]
        public async Task<IActionResult> Update(int authorId, [FromBody] AuthorCreateDto updatedAuthor)
        {
            
             await _authorService.UpdateAsync(authorId, updatedAuthor);
             return Ok("Updated");
        }
    }
}
