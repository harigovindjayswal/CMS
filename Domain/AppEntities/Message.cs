using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Message
{
    [Key]
    public int MessageId { get; set; }

    public int CaseId { get; set; }

    public string FromUserId { get; set; } = null!;

    public string ToUserId { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? SentAt { get; set; }
}
