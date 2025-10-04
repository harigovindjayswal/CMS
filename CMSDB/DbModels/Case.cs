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

    public virtual AspNetUser? AssignedToNavigation { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
