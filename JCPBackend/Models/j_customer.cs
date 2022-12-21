using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JCPBackend.Models;

public partial class j_customer
{
    public string id { get; set; } = null!;

    public string title { get; set; } = null!;

    public string name { get; set; } = null!;

    public string surname { get; set; } = null!;

    public string mobile_no { get; set; } = null!;

    public string? home_no { get; set; }

    public string? work_no { get; set; }

    public string? alt_no { get; set; }

    public string email { get; set; } = null!;

    public string? type { get; set; }

    public string? reg_number { get; set; }

    public string? vat_no { get; set; }

    public string? company_name { get; set; }

    public string? address_line_1 { get; set; }

    public string? address_line_2 { get; set; }

    public string? address_line_3 { get; set; }

    public string? postal { get; set; }

    public string? site_access_id { get; set; }

    public virtual ICollection<j_quote> j_quotes { get; } = new List<j_quote>();
}
