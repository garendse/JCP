using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_site
{
    public string id { get; set; } = null!;

    public string description { get; set; } = null!;

    public virtual ICollection<j_user> users { get; } = new List<j_user>();
}
