namespace Application.LawyerAdmin.DTO;

public class ManagedLawyerDto
{
    public int LawyerId { get; set; }
    public string UserId { get; set; } = null!;
    public string? EmailId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobileNo { get; set; }
    public int? StateId { get; set; }
    public int? CityId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
}

