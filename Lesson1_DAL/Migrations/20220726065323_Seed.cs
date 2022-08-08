using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lesson1_DAL.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("10d657b6-1f0b-406d-9ab9-77814f6c0b60"), "Librarian" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c07abc2f-277e-45d9-9b49-ebd6bd051916"), "Reader" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("06731bcc-94ae-4d95-9343-e5166c055390"), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("06731bcc-94ae-4d95-9343-e5166c055390"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("10d657b6-1f0b-406d-9ab9-77814f6c0b60"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c07abc2f-277e-45d9-9b49-ebd6bd051916"));
        }
    }
}
