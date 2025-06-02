using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject2.Models;

namespace MyProject2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyProject2.Models.Car> Car { get; set; } = default!;
        public DbSet<MyProject2.Models.Driver> Driver { get; set; } = default!;
        public DbSet<MyProject2.Models.Order> Order { get; set; } = default!;
    }
}
