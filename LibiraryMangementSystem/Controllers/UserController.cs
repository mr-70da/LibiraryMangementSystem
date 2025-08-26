using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
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
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            await _userService.CreateAsync(user);
            return Ok("User created successfully.");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> BorrowingHistory(int userId)
        {
            List<UserBorrowingHistoryDto>BorrowingHis;
            BorrowingHis = await _userService.GetBorrowingHistoryAsync(userId);
            return Ok(BorrowingHis);
        }
    }
}
