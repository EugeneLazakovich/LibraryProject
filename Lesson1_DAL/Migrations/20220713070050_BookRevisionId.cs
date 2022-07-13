using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lesson1_DAL.Migrations
{
    public partial class BookRevisionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryBooks_BookRevisions_BookRevisionId",
                table: "LibraryBooks");

            migrationBuilder.DropColumn(
                name: "RevisionId",
                table: "LibraryBooks");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookRevisionId",
                table: "LibraryBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryBooks_BookRevisions_BookRevisionId",
                table: "LibraryBooks",
                column: "BookRevisionId",
                principalTable: "BookRevisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryBooks_BookRevisions_BookRevisionId",
                table: "LibraryBooks");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookRevisionId",
                table: "LibraryBooks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "RevisionId",
                table: "LibraryBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryBooks_BookRevisions_BookRevisionId",
                table: "LibraryBooks",
                column: "BookRevisionId",
                principalTable: "BookRevisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
