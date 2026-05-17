using Domain.AppEntities;

namespace Application.Cases.DTO;

public class UpdateCaseStatusDto
{
    public int CaseId { get; set; }
     public CaseStatus Status { get; set; }
    public CaseStage Stage { get; set; }
}

