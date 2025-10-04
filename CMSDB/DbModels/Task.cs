using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Task
{
    public int TaskId { get; set; }

    public int CaseId { get; set; }

    public string AssignedTo { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual AspNetUser AssignedToNavigation { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;
}
