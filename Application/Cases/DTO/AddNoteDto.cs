namespace Application.Cases.DTO;

public class AddNoteDto
{
    public int CaseId { get; set; }
    public string Content { get; set; } = null!;
    public bool IsPrivate { get; set; }
}

