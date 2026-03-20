namespace Application.MasterData.Courts.DTO;

public class CourtDto
{
    public int CourtId { get; set; }
    public int CourtTypeId { get; set; }
    public int CityId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}

