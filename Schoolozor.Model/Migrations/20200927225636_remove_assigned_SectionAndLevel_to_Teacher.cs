using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolozor.Model.Migrations
{
    public partial class remove_assigned_SectionAndLevel_to_Teacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLevel_SchoolTeacher_SchoolTeacherId",
                table: "SchoolLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSection_SchoolTeacher_SchoolTeacherId",
                table: "SchoolSection");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSection_SchoolTeacherId",
                table: "SchoolSection");

            migrationBuilder.DropIndex(
                name: "IX_SchoolLevel_SchoolTeacherId",
                table: "SchoolLevel");

            migrationBuilder.DropColumn(
                name: "SchoolTeacherId",
                table: "SchoolSection");

            migrationBuilder.DropColumn(
                name: "SchoolTeacherId",
                table: "SchoolLevel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolTeacherId",
                table: "SchoolSection",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolTeacherId",
                table: "SchoolLevel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSection_SchoolTeacherId",
                table: "SchoolSection",
                column: "SchoolTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolLevel_SchoolTeacherId",
                table: "SchoolLevel",
                column: "SchoolTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLevel_SchoolTeacher_SchoolTeacherId",
                table: "SchoolLevel",
                column: "SchoolTeacherId",
                principalTable: "SchoolTeacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSection_SchoolTeacher_SchoolTeacherId",
                table: "SchoolSection",
                column: "SchoolTeacherId",
                principalTable: "SchoolTeacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
