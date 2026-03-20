namespace Application.Cases.DTO;

public class UpdateCaseStatusDto
{
    public int CaseId { get; set; }
    public string Status { get; set; } = null!;
    public string Stage { get; set; } = null!;
}

