using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTo;
using WebApplicationAPI.IServices;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            try
            {
                var result = await _authService.LoginUser(dto);
                if (result.Item1 == 0)
                {
                    return NotFound(result.Item2);
                }
                if (result.Item1 == 1)
                {
                    return BadRequest(result.Item2);
                }
                return Ok(result.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
