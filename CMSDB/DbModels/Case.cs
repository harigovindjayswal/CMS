using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Case
{
    public int CaseId { get; set; }

    public int ClientId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? CaseType { get; set; }

    public string? CourtName { get; set; }

    public string? Opponent { get; set; }

    public string Status { get; set; } = null!;

    public string Stage { get; set; } = null!;

    public string? AssignedTo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
