namespace Application.LawyerAdmin.DTO;

public class ManagedStaffDto
{
    public int StaffId { get; set; }
    public string UserId { get; set; } = null!;
    public string? EmailId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobileNo { get; set; }
    public int LawyerId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
}

