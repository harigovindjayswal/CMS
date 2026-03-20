using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class CaseTypeMst
{
    [Key]
    public int CaseTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

