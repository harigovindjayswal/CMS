namespace Application.MasterData.CourtTypes.DTO;

public class CourtTypeDto
{
    public int CourtTypeId { get; set; }
    public string TypeName { get; set; } = null!;
    public bool IsActive { get; set; }
}

