using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Otpdtl
{
    [Key]
    public int Sno { get; set; }

    public string? Otp { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailId { get; set; }

    public string? ApplicationNo { get; set; }

    public string? UserId { get; set; }

    public string? Flag { get; set; }

    public DateTime? InstDate { get; set; }

    public bool? IsActive { get; set; }
}
