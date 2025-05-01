using System;
using System.Collections.Generic;

namespace CMS.Models;

public partial class Payment
{
    public string Paymentid { get; set; } = null!;

    public string Billid { get; set; } = null!;

    public DateOnly Paymentdate { get; set; }

    public decimal Amountpaid { get; set; }

    public string? Paymentmethod { get; set; }

    public virtual Bill Bill { get; set; } = null!;
}
