using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_job_code
{
    public string code { get; set; } = null!;

    public string site_access_id { get; set; } = null!;

    public string description { get; set; } = null!;

    public string? location { get; set; }

    public int cost { get; set; }

    public int markup { get; set; }

    public int standard_hours { get; set; }

    public int standard_volume { get; set; }

    public int labour_rate { get; set; }
}
