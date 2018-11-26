using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationGuideline.data.Migrations
{
    public partial class Creatingdatabasewithtypegraduationanddeadlines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deadline",
                columns: table => new
                {
                    Semester = table.Column<string>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    GS8 = table.Column<DateTime>(nullable: false),
                    ProQuest = table.Column<DateTime>(nullable: false),
                    FinalVisit = table.Column<DateTime>(nullable: false),
                    FinalExam = table.Column<DateTime>(nullable: false),
                    Survey = table.Column<DateTime>(nullable: false),
                    Graduation = table.Column<DateTime>(nullable: false),
                    Commencement = table.Column<DateTime>(nullable: false),
                    Hooding = table.Column<DateTime>(nullable: false),
                    Audit = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deadline", x => new { x.Semester, x.year });
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    StudentType = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Admin = table.Column<bool>(nullable: false),
                    Semester = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    StepName = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => new { x.Username, x.StepName });
                    table.ForeignKey(
                        name: "FK_Step_User_Username",
                        column: x => x.Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deadline");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
