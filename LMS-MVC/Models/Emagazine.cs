using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class Emagazine
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Issn { get; set; } = null!;

    public string Writer { get; set; } = null!;

    public string PathOfFile { get; set; } = null!;

    public string CoverImage { get; set; } = null!;
}
