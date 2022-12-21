using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_vehicle
{
    public string id { get; set; } = null!;

    public string customer_id { get; set; } = null!;

    public string vin_number { get; set; } = null!;

    public string engine_number { get; set; } = null!;

    public string registration { get; set; } = null!;

    public string brand { get; set; } = null!;

    public string model { get; set; } = null!;

    public int year { get; set; }

    public string? site_access_id { get; set; }

    public virtual ICollection<j_quote> j_quotes { get; } = new List<j_quote>();
}
