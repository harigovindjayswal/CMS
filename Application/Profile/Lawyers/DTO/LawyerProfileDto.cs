using System;

namespace Application.Profile.Lawyers.DTO;

public class LawyerProfileDto
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public string? EmailId { get; set; }
    public string? MobileNo { get; set; }
    public string? Address { get; set; }

    public int? StateId { get; set; }
    public int? CityId { get; set; }

    public string? BarLicenseNumber { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? CourtDetails { get; set; }
}

