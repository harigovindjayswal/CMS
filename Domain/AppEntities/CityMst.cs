using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class CityMst
{
    [Key]
    public int CityId { get; set; }

    public int DistrictId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

