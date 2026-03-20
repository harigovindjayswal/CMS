namespace Application.MasterData.Districts.DTO;

public class DistrictDto
{
    public int DistrictId { get; set; }
    public int StateId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}

