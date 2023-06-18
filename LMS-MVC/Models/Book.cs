using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Isbn { get; set; } = null!;

    public double Price { get; set; }

    public string Author { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public int Stock { get; set; }

    public int Available { get; set; }

    public int Edition { get; set; }

    public string? Image { get; set; }

    public string AddBookByLibrarianId { get; set; } = null!;

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
}
