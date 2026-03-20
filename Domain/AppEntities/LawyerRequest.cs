using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class LawyerRequest
{
    [Key]
    public int LawyerRequestId { get; set; }

    public int ClientId { get; set; }

    public int LawyerId { get; set; }

    public int CaseTypeId { get; set; }

    public int StateId { get; set; }
    public int DistrictId { get; set; }
    public int CityId { get; set; }

    public string CaseDescription { get; set; } = null!;

    public LawyerRequestStatus Status { get; set; } = LawyerRequestStatus.Pending;

    public string? LawyerRemark { get; set; }

    public bool IsActive { get; set; } = true;

    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

