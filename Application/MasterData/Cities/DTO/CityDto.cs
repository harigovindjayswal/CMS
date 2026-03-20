namespace Application.MasterData.Cities.DTO;

public class CityDto
{
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}

