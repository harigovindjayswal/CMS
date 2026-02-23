using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;
public partial class Document
{
    [Key]
    public int DocumentId { get; set; }

    public int CaseId { get; set; }

    public string? Title { get; set; }

    public string FilePath { get; set; } = null!;

    public string? Category { get; set; }

    public string UploadedBy { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public int? Version { get; set; }
}
