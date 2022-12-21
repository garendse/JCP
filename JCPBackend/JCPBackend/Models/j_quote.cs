using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_quote
{
    public string id { get; set; } = null!;

    public string ro_number { get; set; } = null!;

    public string branch_id { get; set; } = null!;

    public string customer_id { get; set; } = null!;

    public string vehicle_id { get; set; } = null!;

    public string create_user_id { get; set; } = null!;

    public DateTime create_datetime { get; set; }

    public string update_user_id { get; set; } = null!;

    public DateTime update_datetime { get; set; }

    public string status { get; set; } = null!;

    public int checkin_odometer { get; set; }

    public string? tech_id { get; set; }

    public string? site_access { get; set; }

    public virtual j_user create_user { get; set; } = null!;

    public virtual j_customer customer { get; set; } = null!;

    public virtual ICollection<j_quote_item> items { get; } = new List<j_quote_item>();

    public virtual j_tech? tech { get; set; }

    public virtual j_user update_user { get; set; } = null!;

    public virtual j_vehicle vehicle { get; set; } = null!;
}
