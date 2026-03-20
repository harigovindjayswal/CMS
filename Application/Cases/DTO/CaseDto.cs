using System;

namespace Application.Cases.DTO;

public class CaseDto
{
    public int CaseId { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }

    public int? LawyerRequestId { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? CaseType { get; set; }
    public string? CourtName { get; set; }
    public string? CaseNumber { get; set; }
    public string? Purpose { get; set; }
    public DateTime? FilingDate { get; set; }

    public string Status { get; set; } = null!;
    public string Stage { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

