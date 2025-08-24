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
        public IActionResult Create([FromBody] UserCreateDto user)
        {   

            _userService.Create(user);
            return Ok("User created successfully.");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            
            // Logic to get user by ID
            return Ok(_userService.GetAll());
        }
        [HttpGet]
        public IActionResult BorrowingHistory(int userId)
        {
           
            return Ok(_userService.GetBorrowingHistory(userId));
        }
    }
}
