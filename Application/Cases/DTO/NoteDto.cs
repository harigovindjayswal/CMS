using System;

namespace Application.Cases.DTO;

public class NoteDto
{
    public int NoteId { get; set; }
    public string UserId { get; set; } = null!;
    public string? Content { get; set; }
    public bool? IsPrivate { get; set; }
    public DateTime? CreatedAt { get; set; }
}

