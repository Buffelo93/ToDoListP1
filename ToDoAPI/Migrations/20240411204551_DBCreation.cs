using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoAPI.Migrations
{
    /// <inheritdoc />
    public partial class DBCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    ToDoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.ToDoId);
                });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "ToDoId", "DeadLine", "IsComplete", "Note", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "1st Note", "1st Title" },
                    { 2, new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "2nd Note", "2nd Title" },
                    { 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "3rd Note", "3rd Title" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItems");
        }
    }
}
