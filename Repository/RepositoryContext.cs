using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiceParadiceApi.Models;

public class RepositoryContext: DbContext
{
    public DbSet<BoardGame> BoardGames { get; set; }
    public DbSet<User> Users { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Store");
        
        modelBuilder.Entity<BoardGame>()
            .ToContainer("BoardGames")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.Id);
        
        modelBuilder.Entity<User>()
            .ToContainer("Users")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.Id);
    }
}