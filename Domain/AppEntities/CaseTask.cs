using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class CaseTask
{
    [Key]
    public int TaskId { get; set; }

    public int CaseId { get; set; }

    public string AssignedTo { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedAt { get; set; }
}
