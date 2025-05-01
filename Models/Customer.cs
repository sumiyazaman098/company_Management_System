using System;
using System.Collections.Generic;

namespace CMS.Models;

public partial class Customer
{
    public string Customerid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public virtual ICollection<Meter> Meters { get; set; } = new List<Meter>();
}
