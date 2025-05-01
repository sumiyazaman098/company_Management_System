using System;
using System.Collections.Generic;

namespace CMS.Models;

public partial class Reading
{
    public string Readingid { get; set; } = null!;

    public string Meterid { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int Unitsconsumed { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Meter Meter { get; set; } = null!;
}
