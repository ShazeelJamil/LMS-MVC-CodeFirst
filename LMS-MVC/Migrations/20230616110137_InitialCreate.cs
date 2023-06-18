using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ISBN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Author = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Publisher = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<int>(type: "int", nullable: false),
                    Edition = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    AddBookByLibrarianID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__books__3DE0C207F391A38A", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "ebooks",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ISBN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Author = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Edition = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PathOfFile = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CoverImage = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ebooks__3DE0C207355DE088", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "emagazines",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ISSN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Writer = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PathOfFile = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CoverImage = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__emagazin__3DE0C20735C48AF8", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Contact = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3214EC07B6BD8369", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Cnic = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "date", nullable: false),
                    Qualification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admins__719FE488931996F1", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK__admins__UserId__2E1BDC42",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "librarians",
                columns: table => new
                {
                    LibrarianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnic = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "date", nullable: false),
                    Qualification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Gender = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    WorkShift = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__libraria__E4D86D7DA891E8A2", x => x.LibrarianId);
                    table.ForeignKey(
                        name: "FK__librarian__UserI__2B3F6F97",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Session = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Degree = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Department = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SessionStartYear = table.Column<int>(type: "int", nullable: false),
                    SessionEndYear = table.Column<int>(type: "int", nullable: false),
                    Fine = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__students__32C52B99EC193EE7", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK__students__UserId__276EDEB3",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "borrows",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    StudentName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LibrarianId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fine = table.Column<double>(type: "float", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__borrows__4295F83F5A120B95", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK__borrows__BookId__3A81B327",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK__borrows__Librari__3B75D760",
                        column: x => x.LibrarianId,
                        principalTable: "librarians",
                        principalColumn: "LibrarianId");
                    table.ForeignKey(
                        name: "FK__borrows__Student__398D8EEE",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_UserId",
                table: "admins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_borrows_BookId",
                table: "borrows",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_borrows_LibrarianId",
                table: "borrows",
                column: "LibrarianId");

            migrationBuilder.CreateIndex(
                name: "IX_borrows_StudentId",
                table: "borrows",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_librarians_UserId",
                table: "librarians",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_students_UserId",
                table: "students",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "borrows");

            migrationBuilder.DropTable(
                name: "ebooks");

            migrationBuilder.DropTable(
                name: "emagazines");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "librarians");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
