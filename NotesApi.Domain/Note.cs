using System.ComponentModel.DataAnnotations;

namespace NotesApi.Domain;

public class Note
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [RegularExpression(@".*\S.*")]
    public string Title { get; set; } = string.Empty;

    [StringLength(4000)]
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
