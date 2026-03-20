namespace Application.LawyerRequests.DTO;

public class CreateLawyerRequestDto
{
    public int LawyerId { get; set; }
    public int CaseTypeId { get; set; }

    public int StateId { get; set; }
    public int DistrictId { get; set; }
    public int CityId { get; set; }

    public string CaseDescription { get; set; } = null!;
}

