namespace Application.LawyerAdmin.DTO;

public class CreateManagedLawyerDto
{
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? MobileNo { get; set; }
    public int? StateId { get; set; }
    public int? CityId { get; set; }
    public string? BarLicenseNumber { get; set; }
}

