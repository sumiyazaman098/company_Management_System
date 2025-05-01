using System;
using System.Collections.Generic;

namespace CMS.Models;

public partial class Meter
{
    public string Meterid { get; set; } = null!;

    public string Customerid { get; set; } = null!;

    public string Meternumber { get; set; } = null!;

    public string? Metertype { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
