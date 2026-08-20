using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Api.Models;
using NotesApi.Application;
using NotesApi.Domain;

namespace NotesApi.Api.Controllers;

[Authorize]
[ApiController]
[Route("notes")]
public class NotesController : ControllerBase
{
    private readonly NoteService _noteService;

    public NotesController(NoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Note>>> GetNotes()
    {
        return await _noteService.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
        var note = await _noteService.GetByIdAsync(id);

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

        var created = await _noteService.CreateAsync(note);

        return CreatedAtAction(nameof(GetNote), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateNote(int id, UpdateNoteRequest request)
    {
        var note = await _noteService.GetByIdAsync(id);
        if (note is null)
        {
            return NotFound();
        }

        note.Title = request.Title.Trim();
        note.Content = request.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteService.UpdateAsync(note);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var deleted = await _noteService.DeleteAsync(id);

        return deleted ? NoContent() : NotFound();
    }
}