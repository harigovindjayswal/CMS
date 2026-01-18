using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Note
{
    public int NoteId { get; set; }

    public int CaseId { get; set; }

    public string UserId { get; set; } = null!;

    public string? Content { get; set; }

    public bool? IsPrivate { get; set; }

    public DateTime? CreatedAt { get; set; }
}
