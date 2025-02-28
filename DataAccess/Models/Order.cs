using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public string? CustomerId { get; set; }

    public decimal? TotalPayment { get; set; }
}
