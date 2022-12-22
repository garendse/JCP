using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JCPBackend.Models;

public partial class j_tech
{
    public string id { get; set; } = null!;

    public string name { get; set; } = null!;

    public string surname { get; set; } = null!;

    public string? site_access_id { get; set; }

    [JsonIgnore]
    public virtual ICollection<j_quote> j_quotes { get; } = new List<j_quote>();
}
