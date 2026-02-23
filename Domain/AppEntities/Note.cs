using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class Note
{
    [Key]
    public int NoteId { get; set; }

    public int CaseId { get; set; }

    public string UserId { get; set; } = null!;

    public string? Content { get; set; }

    public bool? IsPrivate { get; set; }

    public DateTime? CreatedAt { get; set; }
}
