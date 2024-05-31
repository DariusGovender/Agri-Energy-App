using System;
using System.Collections.Generic;

namespace Agri_Energy_Application.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}
