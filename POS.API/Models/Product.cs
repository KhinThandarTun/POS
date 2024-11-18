using System;
using System.Collections.Generic;

namespace POS.API.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    public int? Qty { get; set; }

    public double? Price { get; set; }
}
