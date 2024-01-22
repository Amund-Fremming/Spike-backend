using Microsoft.EntityFrameworkCore;
using Model;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Question>().HasKey(e => e.Id);
    }
}