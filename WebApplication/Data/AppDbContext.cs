using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplicationAPI.Entities;

namespace WebApplicationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<User> AccountUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
