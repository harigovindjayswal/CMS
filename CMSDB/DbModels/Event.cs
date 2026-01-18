using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Event
{
    public int EventId { get; set; }

    public int CaseId { get; set; }

    public string? Title { get; set; }

    public string? EventType { get; set; }

    public DateTime EventDate { get; set; }

    public bool? Reminder { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
