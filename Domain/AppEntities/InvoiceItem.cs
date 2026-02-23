using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class InvoiceItem
{
    [Key]
    public int ItemId { get; set; }

    public int InvoiceId { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? Total { get; set; }
}
