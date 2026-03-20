namespace Application.LawyerAdmin.DTO;

public class CreateManagedCaseDto
{
    public int ClientId { get; set; }
    public int LawyerId { get; set; }
    public int CaseTypeId { get; set; }

    public int? CourtId { get; set; }
    public string? CourtName { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? CaseNumber { get; set; }
    public string? Purpose { get; set; }
    public DateTime? FilingDate { get; set; }
}

