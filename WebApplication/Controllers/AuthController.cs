using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTo;
using WebApplicationAPI.GenericResponse;
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
                    //return NotFound(result.Item2);
                    return NotFound(ResponseResult<TokenDto>.Failure(result.Item2, result.Item2.Message));//generic response we get response in json format with status code and message
                }
                if (result.Item1 == 1)
                {
                    //return BadRequest(result.Item2);
                    return BadRequest(ResponseResult<TokenDto>.Failure(result.Item2, result.Item2.Message));
                }
                //return Ok(result.Item2);
                return Ok(ResponseResult<TokenDto>.Success(result.Item2, result.Item2.Message));
            }
            catch (Exception ex)
            {
                throw;
            }
        }       

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            try
            {
                // Check if the user already exists
                var existingUser = await _authService.RegisterUser(dto);
                if (existingUser.Item1 == 0)
                {
                    return Ok(ResponseResult<string>.Failure(null, existingUser.Item2));
                }
                return Ok(ResponseResult<string>.Success(null, existingUser.Item2));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
