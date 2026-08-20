using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesApi.Domain;

namespace NotesApi.Infrastructure.Data;

public class NotesDbContext : IdentityDbContext<IdentityUser>
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>(entity =>
        {
            entity.Property(note => note.Title).HasMaxLength(150).IsRequired();
            entity.Property(note => note.Content).HasMaxLength(4000);
            entity.Property(note => note.CreatedAt).IsRequired();
            entity.Property(note => note.UpdatedAt).IsRequired();
        });
    }
}