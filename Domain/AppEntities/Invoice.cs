using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public int CaseId { get; set; }

    public int ClientId { get; set; }

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public DateTime? IssuedAt { get; set; }

    public DateTime? DueDate { get; set; }
}
