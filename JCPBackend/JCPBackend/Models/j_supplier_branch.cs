using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_supplier_branch
{
    public string id { get; set; } = null!;

    public string? supplier_id { get; set; }

    public string name { get; set; } = null!;

    public string? address_line_1 { get; set; }

    public string? address_line_2 { get; set; }

    public string? address_line_3 { get; set; }

    public string? postal { get; set; }

    public decimal? lat { get; set; }

    public decimal? lgn { get; set; }

    public string? contact_person { get; set; }

    public string? contact_number { get; set; }

    public string? email { get; set; }

    public virtual ICollection<j_quote_item_supplier> j_quote_item_suppliers { get; } = new List<j_quote_item_supplier>();

    public virtual j_supplier? supplier { get; set; }
}
