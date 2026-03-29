namespace Application.Profile.Staff.DTO;

public class StaffProfileDto
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public string? EmailId { get; set; }
    public string? MobileNo { get; set; }
    public string? Address { get; set; }

    public int LawyerId { get; set; }
}

