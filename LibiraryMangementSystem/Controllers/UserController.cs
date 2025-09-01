using System.Security.Claims;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            await _userService.CreateAsync(user);
            return Ok(await _userService.CreateAsync(user));
        }
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }
        [HttpGet]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> BorrowingHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            return Ok(await _userService.GetBorrowingHistoryAsync(int.Parse(userId)));
        }
    }
}
