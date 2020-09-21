using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolozor.Model.Migrations
{
    public partial class add_teachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentRecord_AspNetUsers_AdviserId",
                table: "StudentRecord");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdviserId",
                table: "StudentRecord",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolTeacherId",
                table: "SchoolSection",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolTeacherId",
                table: "SchoolLevel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolTeacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolTeacher_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSection_SchoolTeacherId",
                table: "SchoolSection",
                column: "SchoolTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolLevel_SchoolTeacherId",
                table: "SchoolLevel",
                column: "SchoolTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTeacher_UserId",
                table: "SchoolTeacher",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRecord_SchoolTeacher_AdviserId",
                table: "StudentRecord",
                column: "AdviserId",
                principalTable: "SchoolTeacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLevel_SchoolTeacher_SchoolTeacherId",
                table: "SchoolLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSection_SchoolTeacher_SchoolTeacherId",
                table: "SchoolSection");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRecord_SchoolTeacher_AdviserId",
                table: "StudentRecord");

            migrationBuilder.DropTable(
                name: "SchoolTeacher");

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

            migrationBuilder.AlterColumn<string>(
                name: "AdviserId",
                table: "StudentRecord",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRecord_AspNetUsers_AdviserId",
                table: "StudentRecord",
                column: "AdviserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
