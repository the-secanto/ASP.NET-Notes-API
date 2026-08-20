using NotesApi.Domain;

namespace NotesApi.Application;

public class NoteService
{
    private readonly INoteRepository _repository;

    public NoteService(INoteRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Note>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Note?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Note> CreateAsync(Note note)
    {
        return _repository.CreateAsync(note);
    }

    public Task<bool> UpdateAsync(Note note)
    {
        return _repository.UpdateAsync(note);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}