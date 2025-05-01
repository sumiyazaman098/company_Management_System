using System;
using System.Collections.Generic;

namespace CMS.Models;

public partial class Bill
{
    public string Billid { get; set; } = null!;

    public string Readingid { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly Duedate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Reading Reading { get; set; } = null!;
}
