using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Voter> Voters { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<SpinGame> SpinGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasKey(g => g.GameId);

        modelBuilder.Entity<SpinGame>()
            .HasKey(sg => sg.GameId);

        modelBuilder.Entity<Question>()
            .HasKey(q => q.Id);

        modelBuilder.Entity<Voter>()
            .HasKey(v => v.VoterId);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Game)
            .WithMany(g => g.Questions)
            .HasForeignKey(q => q.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Voter>()
            .HasOne(v => v.Game)
            .WithMany(g => g.Voters)
            .HasForeignKey(v => v.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Device>()
            .HasKey(d => d.Id);
    }
}
