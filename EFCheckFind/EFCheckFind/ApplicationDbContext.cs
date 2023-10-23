using EFCheckFind.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCheckFind;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=.;Database=TestEF8Db;MultipleActiveResultSets=True;Integrated Security=true;TrustServerCertificate=True");

        optionsBuilder.LogTo(Console.WriteLine);
    }


    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }
}
