using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class Message
{
    public int MessageId { get; set; }

    public int CaseId { get; set; }

    public string FromUserId { get; set; } = null!;

    public string ToUserId { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual AspNetUser FromUser { get; set; } = null!;

    public virtual AspNetUser ToUser { get; set; } = null!;
}
