namespace Application.LawyerAdmin.DTO;

public class ManagedClientDto
{
    public int ClientId { get; set; }
    public string? UserId { get; set; }
    public string? EmailId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobileNo { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
}

