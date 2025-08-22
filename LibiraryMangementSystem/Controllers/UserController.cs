using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        IActionResult Create([FromBody] UserCreateDto user)
        {
            
            return Ok("User created successfully.");
        }
        [HttpGet]
        IActionResult GetAll()
        {
            // Logic to get user by ID
            return Ok("User details retrieved successfully.");
        }
        [HttpGet]
        IActionResult GetBorrowingHistory(int userId)
        {
            // Logic to get borrowing history by user ID
            return Ok("Borrowing history retrieved successfully.");
        }
    }
}
