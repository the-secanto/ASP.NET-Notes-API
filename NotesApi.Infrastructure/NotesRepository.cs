using Microsoft.EntityFrameworkCore;
using NotesApi.Application;
using NotesApi.Domain;
using NotesApi.Infrastructure.Data;

namespace NotesApi.Infrastructure;

public class NotesRepository : INoteRepository
{
    private readonly NotesDbContext _dbContext;

    public NotesRepository(NotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Note>> GetAllAsync()
    {
        return await _dbContext.Notes.AsNoTracking().ToListAsync();
    }

    public async Task<Note?> GetByIdAsync(int id)
    {
        return await _dbContext.Notes.AsNoTracking().FirstOrDefaultAsync(note => note.Id == id);
    }

    public async Task<Note> CreateAsync(Note note)
    {
        _dbContext.Notes.Add(note);
        await _dbContext.SaveChangesAsync();
        return note;
    }

    public async Task<bool> UpdateAsync(Note note)
    {
        _dbContext.Notes.Update(note);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        if (note is null)
        {
            return false;
        }

        _dbContext.Notes.Remove(note);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}