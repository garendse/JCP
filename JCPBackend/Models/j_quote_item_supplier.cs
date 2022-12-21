using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_quote_item_supplier
{
    public string id { get; set; } = null!;

    public string supplier_id { get; set; } = null!;

    public string quote_item_id { get; set; } = null!;

    public int quoted_price { get; set; }

    public string? part_number { get; set; }

    public int count { get; set; }

    public string quoted_by { get; set; } = null!;

    public DateTime quoted_datetime { get; set; }

    public DateTime? accepted_datetime { get; set; }

    public string? accepted_by_user_id { get; set; }

    public string? remarks { get; set; }

    public virtual j_user? user { get; set; }

    public virtual j_quote_item quote_item { get; set; } = null!;

    public virtual j_supplier_branch supplier { get; set; } = null!;
}
