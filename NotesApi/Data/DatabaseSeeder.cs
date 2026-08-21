using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(NotesDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        if (await dbContext.Notes.AnyAsync())
        {
            return;
        }

        var sampleNotes = new[]
        {
            new Note
            {
                Title = "Welcome",
                Content = "This note was created automatically by the database seeding utility.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Note
            {
                Title = "Next steps",
                Content = "Use the API to create, update, and delete notes in the database.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Note
            {
                Title = "API Documentation",
                Content = "Navigate to /swagger or /scalar to view the OpenAPI interactive endpoint documentation.",
                CreatedAt = DateTime.UtcNow.AddHours(-6),
                UpdatedAt = DateTime.UtcNow.AddHours(-6)
            },
            new Note
            {
                Title = "Database Configuration",
                Content = "PostgreSQL runs on localhost:5402 via Docker Compose with volume persistence enabled.",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Note
            {
                Title = "EF Core Migrations",
                Content = "Run 'dotnet ef database update' to apply outstanding schema updates to PostgreSQL.",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Note
            {
                Title = "Feature Backlog",
                Content = "- [x] JWT Authentication\n- [x] PostgreSQL Seeding\n- [ ] Tagging & Search\n- [ ] Soft Delete",
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddHours(-2)
            },
            new Note
            {
                Title = "Architecture Notes",
                Content = "Consider implementing a repository pattern or MediatR if request handler logic grows complex.",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            }
        };

        dbContext.Notes.AddRange(sampleNotes);
        await dbContext.SaveChangesAsync();
    }
}
