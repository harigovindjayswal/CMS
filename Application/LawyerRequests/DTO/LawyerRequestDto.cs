using Domain.AppEntities;

namespace Application.LawyerRequests.DTO;

public class LawyerRequestDto
{
    public int LawyerRequestId { get; set; }

    public int ClientId { get; set; }
    public string? ClientName { get; set; }

    public int LawyerId { get; set; }
    public string? LawyerName { get; set; }

    public int CaseTypeId { get; set; }
    public string? CaseTypeName { get; set; }

    public int StateId { get; set; }
    public int DistrictId { get; set; }
    public int CityId { get; set; }

    public string CaseDescription { get; set; } = null!;

    public LawyerRequestStatus Status { get; set; }
    public string? LawyerRemark { get; set; }
}

