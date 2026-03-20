using System;

namespace Application.Cases.DTO;

public class DocumentDto
{
    public int DocumentId { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public DateTime? UploadedAt { get; set; }
    public string UploadedBy { get; set; } = null!;
}

