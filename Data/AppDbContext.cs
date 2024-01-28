using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Game> Games { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasKey(g => g.GameId);

        modelBuilder.Entity<Question>()
            .HasKey(q => q.Id);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Game)
            .WithMany(g => g.Questions)
            .HasForeignKey(q => q.GameId);
    }
}