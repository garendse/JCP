using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JCPBackend.Models;

public partial class j_user
{
    public string id { get; set; } = null!;

    public string username { get; set; } = null!;

    public string name { get; set; } = null!;

    public string surname { get; set; } = null!;

    [JsonIgnore]
    public string password { get; set; } = null!;

    public string? tel_no { get; set; }

    public bool active { get; set; }

    public DateTime password_date { get; set; }

    public DateTime end_date { get; set; }

    public string role { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<j_quote_item_supplier> j_quote_item_suppliers { get; } = new List<j_quote_item_supplier>();

    [JsonIgnore]
    public virtual ICollection<j_quote> j_quotecreate_users { get; } = new List<j_quote>();

    [JsonIgnore]
    public virtual ICollection<j_quote> j_quoteupdate_users { get; } = new List<j_quote>();
    
    [JsonIgnore]
    public virtual ICollection<j_site> sites { get; } = new List<j_site>();
}
