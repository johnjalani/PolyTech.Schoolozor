using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolozor.Model.Migrations
{
    public partial class update_guardian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "StudentGuardian");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "StudentGuardian",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "StudentGuardian",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "StudentGuardian");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "StudentGuardian");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "StudentGuardian",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
