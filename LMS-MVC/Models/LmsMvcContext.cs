using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMS_MVC.Models;

public partial class LmsMvcContext : DbContext
{
    public LmsMvcContext()
    {
    }

    public LmsMvcContext(DbContextOptions<LmsMvcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrow> Borrows { get; set; }

    public virtual DbSet<Ebook> Ebooks { get; set; }

    public virtual DbSet<Emagazine> Emagazines { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(@"Data Source=SHAZEEL\SQLEXPRESS;Initial Catalog=LMS-MVC2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admins__719FE488931996F1");

            entity.ToTable("admins");

            entity.Property(e => e.AdminId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DateOfJoining).HasColumnType("date");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__admins__UserId__2E1BDC42");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__books__3DE0C207F391A38A");

            entity.ToTable("books");

            entity.Property(e => e.AddBookByLibrarianId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AddBookByLibrarianID");
            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Publisher)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Borrow>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__borrows__4295F83F5A120B95");

            entity.ToTable("borrows");

            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Book).WithMany(p => p.Borrows)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__borrows__BookId__3A81B327");

            entity.HasOne(d => d.Librarian).WithMany(p => p.Borrows)
                .HasForeignKey(d => d.LibrarianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__borrows__Librari__3B75D760");

            entity.HasOne(d => d.Student).WithMany(p => p.Borrows)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__borrows__Student__398D8EEE");
        });

        modelBuilder.Entity<Ebook>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__ebooks__3DE0C207355DE088");

            entity.ToTable("ebooks");

            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Isbn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.PathOfFile)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Publisher)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Emagazine>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__emagazin__3DE0C20735C48AF8");

            entity.ToTable("emagazines");

            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Issn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ISSN");
            entity.Property(e => e.PathOfFile)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Writer)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.LibrarianId).HasName("PK__libraria__E4D86D7DA891E8A2");

            entity.ToTable("librarians");

            entity.Property(e => e.Cnic)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DateOfJoining).HasColumnType("date");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.WorkShift)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Librarians)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__librarian__UserI__2B3F6F97");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__students__32C52B99EC193EE7");

            entity.ToTable("students");

            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Degree)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Session)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__students__UserId__276EDEB3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC07B6BD8369");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
