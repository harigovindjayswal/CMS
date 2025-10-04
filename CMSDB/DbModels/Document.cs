using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Document
{
    public int DocumentId { get; set; }

    public int CaseId { get; set; }

    public string? Title { get; set; }

    public string FilePath { get; set; } = null!;

    public string? Category { get; set; }

    public string UploadedBy { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public int? Version { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual AspNetUser UploadedByNavigation { get; set; } = null!;
}
