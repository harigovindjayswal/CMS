using System;
using System.Collections.Generic;

namespace CMSDb.DbModels;

public partial class OptionMst
{
    public int SerialNo { get; set; }

    public string? OptionName { get; set; }

    public string? OptionCode { get; set; }

    public string? OptionDesc { get; set; }

    public string? OptionDescHindi { get; set; }

    public bool? IsActive { get; set; }
}
