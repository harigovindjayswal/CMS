using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class InvoiceItem
{
    public int ItemId { get; set; }

    public int InvoiceId { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? Total { get; set; }
}
