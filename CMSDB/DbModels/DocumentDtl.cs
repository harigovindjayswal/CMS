using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class DocumentDtl
{
    public int Id { get; set; }

    public string? ApplicationNo { get; set; }

    public int? DocId { get; set; }

    public string? DocPath { get; set; }

    public bool? IsDigiSign { get; set; }

    public string? DigiPath { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? Uploaddatetime { get; set; }

    public string? Flag { get; set; }
}
