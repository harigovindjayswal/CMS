using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Client
{
    [Key]
    public int ClientId { get; set; }

    public string? UserId { get; set; }

    // For LawyerAdmin-created accounts scoping
    public string? RegisteredByUserId { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? EmailId { get; set; }

    public string? MobileNo { get; set; }

    public string? Address { get; set; }

    public int? State { get; set; }

    public int? District { get; set; }

    public string? City { get; set; }

    public string? PinCode { get; set; }

    public string? Notes { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }
}
