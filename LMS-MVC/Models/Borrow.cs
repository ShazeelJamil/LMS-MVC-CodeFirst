using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Borrow
{
    public int BorrowId { get; set; }

    public string StudentId { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int LibrarianId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public double Fine { get; set; }

    public bool IsReturn { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Librarian Librarian { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
