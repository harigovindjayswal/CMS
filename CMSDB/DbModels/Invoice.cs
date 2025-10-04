using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int CaseId { get; set; }

    public int ClientId { get; set; }

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public DateTime? IssuedAt { get; set; }

    public DateTime? DueDate { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
