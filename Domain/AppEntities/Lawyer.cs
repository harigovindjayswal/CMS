using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Lawyer
{
    [Key]
    public int LawyerId { get; set; }

    // Identity user id
    public string UserId { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public string? EmailId { get; set; }
    public string? MobileNo { get; set; }
    public string? Address { get; set; }

    public int? StateId { get; set; }
    public int? CityId { get; set; }

    public string? ProfileImagePath { get; set; }

    public string? BarLicenseNumber { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? CourtDetails { get; set; }

    // For LawyerAdmin-created accounts scoping
    public string? RegisteredByUserId { get; set; }

    public bool IsActive { get; set; } = true;

    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

