using System;
using System.Collections.Generic;

namespace JCPBackend.Models;

public partial class j_quote_status
{
    public string status { get; set; } = null!;

    public int sort_order { get; set; }
}
