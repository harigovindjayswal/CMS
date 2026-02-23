using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.AppEntities;

public partial class DocumentMaster
{
    [Key]
    public int Id { get; set; }

    public int? DocId { get; set; }

    public string? DocName { get; set; }

    public string? Mandatory { get; set; }

    public string? DigiDocName { get; set; }

    public string? DigiMandatory { get; set; }

    public short? Sort { get; set; }

    public string? FileExtension { get; set; }

    public string? FileSizeBytes { get; set; }

    public string? FileDirectory { get; set; }

    public string? DocDesc { get; set; }

    public string? Flag { get; set; }

    public bool? IsActive { get; set; }
}
