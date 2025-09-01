using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        //Create new auther
        [HttpPost]
        
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto newAuthor)
        {
            return Ok(await _authorService.CreateAsync(newAuthor));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int authorId)
        {            
            return Ok(await _authorService.DeleteAsync(authorId));
        }

        [HttpPut]
        public async Task<IActionResult> Update(AuthorUpdateRequestDto requestDto)
        {
             return Ok(await _authorService.UpdateAsync(requestDto));
        }
    }
}
