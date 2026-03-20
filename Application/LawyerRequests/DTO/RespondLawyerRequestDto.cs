using Domain.AppEntities;

namespace Application.LawyerRequests.DTO;

public class RespondLawyerRequestDto
{
    public int LawyerRequestId { get; set; }
    public LawyerRequestStatus Status { get; set; }
    public string LawyerRemark { get; set; } = null!;
}

