﻿// <auto-generated />
using System;
using LMS_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS_MVC.Migrations
{
    [DbContext(typeof(LmsMvcContext))]
    partial class LmsMvcContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMS_MVC.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("Cnic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("date");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("AdminId")
                        .HasName("PK__admins__719FE488931996F1");

                    b.HasIndex("UserId");

                    b.ToTable("admins", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("AddBookByLibrarianId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("AddBookByLibrarianID");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Available")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Edition")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ISBN");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("BookId")
                        .HasName("PK__books__3DE0C207F391A38A");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Borrow", b =>
                {
                    b.Property<int>("BorrowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BorrowId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<double>("Fine")
                        .HasColumnType("float");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime");

                    b.Property<int>("LibrarianId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("BorrowId")
                        .HasName("PK__borrows__4295F83F5A120B95");

                    b.HasIndex("BookId");

                    b.HasIndex("LibrarianId");

                    b.HasIndex("StudentId");

                    b.ToTable("borrows", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Ebook", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CoverImage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Edition")
                        .HasColumnType("int");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ISBN");

                    b.Property<string>("PathOfFile")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("BookId")
                        .HasName("PK__ebooks__3DE0C207355DE088");

                    b.ToTable("ebooks", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Emagazine", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("CoverImage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Issn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ISSN");

                    b.Property<string>("PathOfFile")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("BookId")
                        .HasName("PK__emagazin__3DE0C20735C48AF8");

                    b.ToTable("emagazines", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Librarian", b =>
                {
                    b.Property<int>("LibrarianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibrarianId"));

                    b.Property<string>("Cnic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("date");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Qualification")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WorkShift")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("LibrarianId")
                        .HasName("PK__libraria__E4D86D7DA891E8A2");

                    b.HasIndex("UserId");

                    b.ToTable("librarians", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Fine")
                        .HasColumnType("float");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Session")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SessionEndYear")
                        .HasColumnType("int");

                    b.Property<int>("SessionStartYear")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("StudentId")
                        .HasName("PK__students__32C52B99EC193EE7");

                    b.HasIndex("UserId");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__users__3214EC07B6BD8369");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("LMS_MVC.Models.Admin", b =>
                {
                    b.HasOne("LMS_MVC.Models.User", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__admins__UserId__2E1BDC42");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_MVC.Models.Borrow", b =>
                {
                    b.HasOne("LMS_MVC.Models.Book", "Book")
                        .WithMany("Borrows")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK__borrows__BookId__3A81B327");

                    b.HasOne("LMS_MVC.Models.Librarian", "Librarian")
                        .WithMany("Borrows")
                        .HasForeignKey("LibrarianId")
                        .IsRequired()
                        .HasConstraintName("FK__borrows__Librari__3B75D760");

                    b.HasOne("LMS_MVC.Models.Student", "Student")
                        .WithMany("Borrows")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__borrows__Student__398D8EEE");

                    b.Navigation("Book");

                    b.Navigation("Librarian");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LMS_MVC.Models.Librarian", b =>
                {
                    b.HasOne("LMS_MVC.Models.User", "User")
                        .WithMany("Librarians")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__librarian__UserI__2B3F6F97");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_MVC.Models.Student", b =>
                {
                    b.HasOne("LMS_MVC.Models.User", "User")
                        .WithMany("Students")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__students__UserId__276EDEB3");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_MVC.Models.Book", b =>
                {
                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("LMS_MVC.Models.Librarian", b =>
                {
                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("LMS_MVC.Models.Student", b =>
                {
                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("LMS_MVC.Models.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Librarians");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}