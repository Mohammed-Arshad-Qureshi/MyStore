using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Interfaces;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authService.Authenticate(request);
            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Only admins can see this.");
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return Ok("Admins and Managers can view this.");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var success = _authService.Register(request);
            if (!success)
                return BadRequest("Username already exists or registration failed.");

            return Ok("Registration successful.");
        }

        [Authorize(Roles = "User")]
        [HttpGet("user-data")]
        public IActionResult GetUserData()
        {
            return Ok("Only regular users can access this.");
        }
    }
}
