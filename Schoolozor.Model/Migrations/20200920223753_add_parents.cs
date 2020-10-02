using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolozor.Model.Migrations
{
    public partial class add_parents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentProfileId",
                table: "StudentGuardian",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentGuardian_StudentProfileId",
                table: "StudentGuardian",
                column: "StudentProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGuardian_StudentProfile_StudentProfileId",
                table: "StudentGuardian",
                column: "StudentProfileId",
                principalTable: "StudentProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGuardian_StudentProfile_StudentProfileId",
                table: "StudentGuardian");

            migrationBuilder.DropIndex(
                name: "IX_StudentGuardian_StudentProfileId",
                table: "StudentGuardian");

            migrationBuilder.DropColumn(
                name: "StudentProfileId",
                table: "StudentGuardian");
        }
    }
}
