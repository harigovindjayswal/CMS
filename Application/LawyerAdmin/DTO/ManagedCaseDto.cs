using Domain.AppEntities;

namespace Application.LawyerAdmin.DTO;

public class ManagedCaseDto
{
    public int CaseId { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }
    public string? AssignedLawyerUserId { get; set; }
    public string? AssignedLawyerName { get; set; }
    public string Title { get; set; } = null!;
    public string? CaseType { get; set; }
    public string? CourtName { get; set; }
    public string? CaseNumber { get; set; }
    public CaseStatus Status { get; set; }
     public CaseStage Stage { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

