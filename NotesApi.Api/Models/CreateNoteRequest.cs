using System.ComponentModel.DataAnnotations;

namespace NotesApi.Api.Models;

public class CreateNoteRequest
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Title cannot be empty.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(4000)]
    public string? Content { get; set; }
}