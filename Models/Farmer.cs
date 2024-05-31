using System;
using System.Collections.Generic;

namespace Agri_Energy_Application.Models;

public partial class Farmer
{
    public int FarmerId { get; set; }

    public string? Email { get; set; }

    public string FullName { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual User? EmailNavigation { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
