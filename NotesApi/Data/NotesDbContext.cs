using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Data;

public class NotesDbContext(DbContextOptions<NotesDbContext> options) : DbContext(options)
{
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.Property(note => note.Title).HasMaxLength(150).IsRequired();
            entity.Property(note => note.Content).HasMaxLength(4000);
            entity.Property(note => note.CreatedAt).IsRequired();
            entity.Property(note => note.UpdatedAt).IsRequired();
        });
    }
}
