using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
  /// <summary>
  /// Anteckning tabell
  /// </summary>
  public DbSet<Note> Notes { get; set; }
  /// <summary>
  /// Kategori tabell
  /// </summary>
  public DbSet<Category> Categories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Note>()
      .HasOne(n => n.Category)
      .WithMany(a => a.Notes)
      .HasForeignKey(n => n.CategoryId);

    modelBuilder.Entity<Note>()
      .HasOne(n => n.User)
      .WithMany(u => u.Notes)
      .HasForeignKey(n => n.UserId);

    modelBuilder.Entity<Category>()
      .HasOne(n => n.User)
      .WithMany(u => u.Categories)
      .HasForeignKey(n => n.UserId);

    base.OnModelCreating(modelBuilder);
  }
}
