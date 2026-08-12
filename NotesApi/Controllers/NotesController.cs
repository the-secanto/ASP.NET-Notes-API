using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Models;

namespace NotesApi.Controllers;

[ApiController]
[Route("notes")]
public class NotesController(NotesDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Note>>> GetNotes()
    {
        return await dbContext.Notes.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
        var note = await dbContext.Notes.FindAsync(id);

        return note is null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(CreateNoteRequest request)
    {
        var note = new Note
        {
            Title = request.Title.Trim(),
            Content = request.Content
        };

        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateNote(int id, UpdateNoteRequest request)
    {
        var note = await dbContext.Notes.FindAsync(id);
        if (note is null)
        {
            return NotFound();
        }

        note.Title = request.Title.Trim();
        note.Content = request.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await dbContext.Notes.FindAsync(id);
        if (note is null)
        {
            return NotFound();
        }

        dbContext.Notes.Remove(note);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
