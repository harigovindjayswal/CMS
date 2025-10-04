using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string UserId { get; set; } = null!;

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Ipaddress { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
