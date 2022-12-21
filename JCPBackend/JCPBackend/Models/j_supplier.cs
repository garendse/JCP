using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_supplier
{
    public string id { get; set; } = null!;

    public string name { get; set; } = null!;

    public string? reg_no { get; set; }

    public string? vat_no { get; set; }

    public string? tax_clearance { get; set; }

    public int? credit_limit { get; set; }

    public int? credit_balance { get; set; }

    public string? tel_num { get; set; }

    public string? address_line_1 { get; set; }

    public string? address_line_2 { get; set; }

    public string? address_line_3 { get; set; }

    public string? postal { get; set; }

    public string? contact_person { get; set; }

    public string? contact_no { get; set; }

    public string? email { get; set; }

    public string? after_hours_no { get; set; }

    public string? standby_person { get; set; }

    public string? standby_no { get; set; }

    public string? standby_email { get; set; }

    public virtual ICollection<j_supplier_branch> supplier { get; } = new List<j_supplier_branch>();
}
