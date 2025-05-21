using JsonPlaceholderDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonPlaceholderDemo.Data
{
    public class TareasDbContext : DbContext
    {
        public TareasDbContext(DbContextOptions<TareasDbContext> options)
            : base(options)
        {
        }

        // Ejemplo de tabla
       public DbSet<Post> Posts { get; set; } = null!;
    }

}
