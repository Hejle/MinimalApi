using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApi.Database.Migrations.Joker
{
    public partial class Jokers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jokers",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JokerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JokeDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokers", x => new { x.JokerName, x.CreatedDate });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jokers");
        }
    }
}
