using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Client
{
    public int ClientId { get; set; }

    public string? UserId { get; set; }

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

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual AspNetUser? User { get; set; }
}
