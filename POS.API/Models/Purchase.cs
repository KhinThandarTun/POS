using System;
using System.Collections.Generic;

namespace POS.API.Models;

public partial class Purchase
{
    public string PurchaseVno { get; set; } = null!;

    public string? SupplierId { get; set; }

    public double? TotalAmt { get; set; }
}
