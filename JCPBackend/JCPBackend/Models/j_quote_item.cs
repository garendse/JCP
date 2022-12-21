using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_quote_item
{
    public string id { get; set; } = null!;

    public string quote_id { get; set; } = null!;

    public int sort_order { get; set; }

    public string? job_code { get; set; }

    public string description { get; set; } = null!;

    public string? location { get; set; }

    public int labour_hours { get; set; }

    public int labour_rate { get; set; }

    public int part_rate { get; set; }

    public int part_markup { get; set; }

    public int part_quantity { get; set; }

    public bool auth { get; set; }

    public virtual ICollection<j_quote_item_supplier> subquotes { get; } = new List<j_quote_item_supplier>();

    public virtual j_quote quote { get; set; } = null!;
}
