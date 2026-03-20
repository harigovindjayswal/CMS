using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Case
{
    [Key]
    public int CaseId { get; set; }

    // Optional: link back to the accepted lawyer request that originated the case
    public int? LawyerRequestId { get; set; }

    public int ClientId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? CaseType { get; set; }

    public string? CourtName { get; set; }

    public string? CaseNumber { get; set; }

    public string? Purpose { get; set; }

    public DateTime? FilingDate { get; set; }

    public string? Opponent { get; set; }

    public string Status { get; set; } = null!;

    public string Stage { get; set; } = null!;

    public string? AssignedTo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
