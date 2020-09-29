using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolozor.Model.Migrations
{
    public partial class remove_sy_in_LevelAndSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLevel_SchoolYear_SchoolYearId",
                table: "SchoolLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSection_SchoolYear_SchoolYearId",
                table: "SchoolSection");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSection_SchoolYearId",
                table: "SchoolSection");

            migrationBuilder.DropIndex(
                name: "IX_SchoolLevel_SchoolYearId",
                table: "SchoolLevel");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "SchoolSection");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "SchoolLevel");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "SchoolSection",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "SchoolLevel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSection_SchoolId",
                table: "SchoolSection",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolLevel_SchoolId",
                table: "SchoolLevel",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLevel_SchoolProfile_SchoolId",
                table: "SchoolLevel",
                column: "SchoolId",
                principalTable: "SchoolProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSection_SchoolProfile_SchoolId",
                table: "SchoolSection",
                column: "SchoolId",
                principalTable: "SchoolProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLevel_SchoolProfile_SchoolId",
                table: "SchoolLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSection_SchoolProfile_SchoolId",
                table: "SchoolSection");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSection_SchoolId",
                table: "SchoolSection");

            migrationBuilder.DropIndex(
                name: "IX_SchoolLevel_SchoolId",
                table: "SchoolLevel");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "SchoolSection");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "SchoolLevel");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "SchoolSection",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "SchoolLevel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSection_SchoolYearId",
                table: "SchoolSection",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolLevel_SchoolYearId",
                table: "SchoolLevel",
                column: "SchoolYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLevel_SchoolYear_SchoolYearId",
                table: "SchoolLevel",
                column: "SchoolYearId",
                principalTable: "SchoolYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSection_SchoolYear_SchoolYearId",
                table: "SchoolSection",
                column: "SchoolYearId",
                principalTable: "SchoolYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
