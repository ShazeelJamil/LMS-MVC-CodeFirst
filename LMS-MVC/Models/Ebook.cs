using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Ebook
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Isbn { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int Edition { get; set; }

    public string Publisher { get; set; } = null!;

    public string PathOfFile { get; set; } = null!;

    public string CoverImage { get; set; } = null!;
}
