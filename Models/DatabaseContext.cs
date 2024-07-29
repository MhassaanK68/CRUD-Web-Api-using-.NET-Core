using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> Options) : base(Options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; } = null!;

    }
}
