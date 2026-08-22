using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTo;
using WebApplicationAPI.IServices;

namespace WebApplicationAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Tuple<int, string>> LoginUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.AccountUsers.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (existingUser == null)
                {
                    return new Tuple<int, string>(0, "This User does not exist, Please login again");
                }
                if(existingUser.Password != dto.Password)
                {
                    return new Tuple<int, string>(1, "Invalid Password");
                }
                return new Tuple<int, string>(2, "Login Successful");

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(3, $"Something went wrong: {ex.Message}");
            }
        }

    }
}
