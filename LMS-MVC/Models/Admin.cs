using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Cnic { get; set; } = null!;

    public DateTime DateOfJoining { get; set; }

    public string Qualification { get; set; } = null!;

    public double Salary { get; set; }

    public string Gender { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
