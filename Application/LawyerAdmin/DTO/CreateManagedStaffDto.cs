namespace Application.LawyerAdmin.DTO;

public class CreateManagedStaffDto
{
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public required string Password { get; set; }

    public int LawyerId { get; set; }

    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? MobileNo { get; set; }
}

