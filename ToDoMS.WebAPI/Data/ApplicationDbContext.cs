using Microsoft.EntityFrameworkCore;
using ToDoMS.WebAPI.Models;

namespace ToDoMS.WebAPI.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; } 
    }
}
