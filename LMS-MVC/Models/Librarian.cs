using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Librarian
{
    public int LibrarianId { get; set; }

    public string Cnic { get; set; } = null!;

    public DateTime DateOfJoining { get; set; }

    public string? Qualification { get; set; }

    public double Salary { get; set; }

    public string Gender { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? WorkShift { get; set; }

    public string UserId { get; set; } = null!;

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    public virtual User User { get; set; } = null!;
}
