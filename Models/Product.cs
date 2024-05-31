using System;
using System.Collections.Generic;

namespace Agri_Energy_Application.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? FarmerId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public DateOnly ProductionDate { get; set; }

    public virtual Farmer? Farmer { get; set; }
}
