using System;
using System.Collections.Generic;

namespace Agri_Energy_Application.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public virtual User? EmailNavigation { get; set; }
}
