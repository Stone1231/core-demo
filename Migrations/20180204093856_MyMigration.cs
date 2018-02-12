using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coreDemo.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClubM",
                columns: table => new
                {
                    Sn = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubM", x => x.Sn);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Sn = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    Hight = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    SingleClassId = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Sn);
                    table.ForeignKey(
                        name: "FK_Student_ClassM_SingleClassId",
                        column: x => x.SingleClassId,
                        principalTable: "ClassM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudClub",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    ClubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudClub", x => new { x.StudentId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_StudClub_ClubM_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubM",
                        principalColumn: "Sn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudClub_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Sn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudClub_ClubId",
                table: "StudClub",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SingleClassId",
                table: "Student",
                column: "SingleClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudClub");

            migrationBuilder.DropTable(
                name: "ClubM");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "ClassM");
        }
    }
}
