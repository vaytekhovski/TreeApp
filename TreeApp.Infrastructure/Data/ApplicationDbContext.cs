using Microsoft.EntityFrameworkCore;
using TreeApp.Domain.Entities;

namespace TreeApp.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Tree> Trees => Set<Tree>();
    public DbSet<Node> Nodes => Set<Node>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tree>()
            .HasIndex(t => t.Name)
            .IsUnique();

        modelBuilder.Entity<Node>()
            .HasOne(n => n.Tree)
            .WithMany(t => t.Nodes)
            .HasForeignKey(n => n.TreeId);

        modelBuilder.Entity<Node>()
            .HasOne(n => n.Parent)
            .WithMany(n => n.Children)
            .HasForeignKey(n => n.ParentId);
    }
}