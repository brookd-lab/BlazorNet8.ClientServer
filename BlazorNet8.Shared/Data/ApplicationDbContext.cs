
using BlazorNet8.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorNet8.Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }
    }
}
