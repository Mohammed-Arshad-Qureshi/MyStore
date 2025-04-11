using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Interfaces;
using MyStore.DTOs;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IForgotPasswordService _service;

        public ForgotPasswordController(IForgotPasswordService service)
        {
            _service = service;
        }

        [HttpPost("request-reset")]
        public IActionResult RequestReset([FromBody] ForgotPasswordRequest request)
        {
            var result = _service.GenerateResetToken(request.Username);
            if (result == "User not found")
                return NotFound(result);

            return Ok(new { ResetLink = result });
        }

        [HttpPost("reset")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = _service.ResetPassword(request.Token, request.NewPassword);
            if (result == "Invalid or expired token")
                return BadRequest(result);

            return Ok(result);
        }
    }
}
