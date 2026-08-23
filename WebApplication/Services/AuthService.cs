using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTo;
using WebApplicationAPI.Entities;
using WebApplicationAPI.IServices;

namespace WebApplicationAPI.Services
{
    public class AuthService : IAuthService
    {
        //here i added Dependency Injection for the AppDbContext to access the database by Constructor Injection
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Tuple<int, TokenDto>> LoginUser(UserDto dto)
        {
            try
            {
                var tokendto = new TokenDto();
                if (dto == null)
                {
                    tokendto.Message = "Please Fill in the required fields";
                    return new Tuple<int, TokenDto>(1, tokendto);
                }
                var existingUser = await _context.AccountUsers.FirstOrDefaultAsync(u => u.Email == dto.Email);//FirstOrDefaultAsync returns the first user that matches the email or null if no user is found
                if (existingUser == null)
                {
                    tokendto.Message = "This User does not exist, Please login again";
                    return new Tuple<int, TokenDto>(0, tokendto);
                }
                var passwordHasher = new PasswordHasher<string>();
                var varifyPassword = passwordHasher.VerifyHashedPassword(dto.Email, existingUser.Password, dto.Password);

                if(varifyPassword == PasswordVerificationResult.Success)
                {
                    UserDto user = new UserDto
                    {
                        Id = existingUser.Id,
                        Name = existingUser.Name,
                        Email = existingUser.Email
                    };
                    var token = GetJWTToken(user);
                    tokendto.Token = token;
                    tokendto.Message = "Login Successful";
                    return new Tuple<int, TokenDto>(2, tokendto);
                }
                else if(varifyPassword == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    UserDto user = new UserDto
                    {
                        Id = existingUser.Id,
                        Name = existingUser.Name,
                        Email = existingUser.Email
                    };
                    var token = GetJWTToken(user);
                    tokendto.Token = token;
                    tokendto.Message = "Login Successful, New Hash Generated";

                    existingUser.Password = PasswordHash(dto);
                    _context.AccountUsers.Update(existingUser);
                    await _context.SaveChangesAsync();
                    return new Tuple<int, TokenDto>(2, tokendto);
                }
                else if(varifyPassword == PasswordVerificationResult.Failed)
                {
                    tokendto.Message = "Invalid Password";
                    return new Tuple<int, TokenDto>(1, tokendto);
                }
                return new Tuple<int, TokenDto>(1, tokendto);
            }
            catch (Exception ex)
            {
                return new Tuple<int, TokenDto>(3, new TokenDto { Token = string.Empty, Message = $"Something went wrong: {ex.Message}" });
            }
        }
        private string GetJWTToken(UserDto dto)
        {
            // Implement JWT token generation logic here
            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, dto.Name),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, dto.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, dto.Id.ToString())
            };
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("HS0ULAGdsEylJ0GGvrLLXqmrxi3ECkaZUhfTG8dvt64"));

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: "vguru-client",
                audience: "vguru-client",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public async Task<Tuple<int, string>> RegisterUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.AccountUsers.AnyAsync(u => u.Email == dto.Email);//AnyAsync returns true or false
                if (existingUser)
                {
                    return new Tuple<int, string>(0, "User Email already exists");
                }
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = PasswordHash(dto)
                };
                _context.AccountUsers.Add(newUser);
                await _context.SaveChangesAsync();
                return new Tuple<int, string>(1, "User registered successfully");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(3, $"Something went wrong: {ex.Message}");
            }
        }
        private string PasswordHash(UserDto dto)
        {
            // Implement your password hashing logic here
            // For example, you can use a hashing algorithm like SHA256 or bcrypt
            var passwordHasher = new PasswordHasher<string>();
            var hash = passwordHasher.HashPassword(dto.Email, dto.Password);
            return hash; // Placeholder, replace with actual hashing
        }
    }
}
