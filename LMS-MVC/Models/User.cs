using System;
using System.Collections.Generic;

namespace LMS_MVC.Models;

public partial class User
{

    public string Id { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Contact { get; set; } = null!;

    public string Image { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Librarian> Librarians { get; set; } = new List<Librarian>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
