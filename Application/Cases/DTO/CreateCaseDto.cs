using System;

namespace Application.Cases.DTO;

public class CreateCaseDto
{
    public int LawyerRequestId { get; set; }
    public int? CourtId { get; set; }
    public string? CourtName { get; set; }
    public int CaseTypeId { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? CaseNumber { get; set; }
    public string? Purpose { get; set; }
    public DateTime? FilingDate { get; set; }
}
