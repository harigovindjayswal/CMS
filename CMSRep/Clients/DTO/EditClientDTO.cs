using System;

namespace CMSApplication.Clients.DTO;

public class EditClientDTO:BaseClientDTO
{
    public int? ClientId { get; set; }
    public bool? IsActive { get; set; }
}
