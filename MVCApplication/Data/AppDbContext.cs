using Microsoft.EntityFrameworkCore;
using MVCApplication.Models;

namespace MVCApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } // created Schema for user class
    }
}
