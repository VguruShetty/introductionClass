using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVCApplication.Data;
using MVCApplication.Dto;
using MVCApplication.Models;

namespace MVCApplication.Controllers
{
    
    public class AuthController(AppDbContext _context) : Controller
    {
        //previous code
        //private readonly AppDbContext _context;
        //public AuthController(AppDbContext context)
        //{
        //    _context = context;
        //}
        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> CreateUser(UserDTo dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                ViewBag.ErrorMessage = "Kindly fill all the details!!!";
                return View("Register");
            }
            var existingUser = await _context.Users.FirstOrDefaultAsync(u=>u.Email == dto.Email);

            if (existingUser == null)
            {
                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User created successfully. Please log in.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.ErrorMessage = ("User with this email already Exists!!!");
                return View("Register");
            }            
        }
        public async Task<IActionResult> LoginUser(UserDTo dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                ViewBag.ErrorMessage = "Kindly fill all the details!!!";
                return View("Login");
            }
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if(existingUser == null)
            {
                ViewBag.ErrorMessage = "User with this Email Does not exist";
                return View("Login");
            }
            else
            {
                if(existingUser.Password == dto.Password)
                {
                    var token = GenerateJwtToken(existingUser.Username ?? existingUser.Email, existingUser.Email);

                    Response.Cookies.Append("JwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect Password";
                    return View("Login");
                }
            }
        }
        private string GenerateJwtToken(string username, string email)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes("JADBO7CTFlh4Ruiks8ZutW8TFdurPmBg6NdEwIQBvsZ");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                 };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public IActionResult LogOutUser()
        {
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login");
        }
    }
}
