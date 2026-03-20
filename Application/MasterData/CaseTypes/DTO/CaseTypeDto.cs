namespace Application.MasterData.CaseTypes.DTO;

public class CaseTypeDto
{
    public int CaseTypeId { get; set; }
    public string TypeName { get; set; } = null!;
    public bool IsActive { get; set; }
}

