﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApi.Database.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Isbn = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PageCount = table.Column<int>(type: "int", nullable: false),
                ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Isbn);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Books");
    }
}