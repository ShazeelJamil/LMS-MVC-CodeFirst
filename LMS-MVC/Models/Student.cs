using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string Session { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string Department { get; set; } = null!;

    public int SessionStartYear { get; set; }

    public int SessionEndYear { get; set; }

    public double Fine { get; set; }

    public bool IsActive { get; set; }

    public string Gender { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    public virtual User User { get; set; } = null!;
}
