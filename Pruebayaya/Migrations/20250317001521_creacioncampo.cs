using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pruebayaya.Migrations
{
    public partial class creacioncampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creadopor",
                table: "Categoria");

            migrationBuilder.AddColumn<DateTime>(
                name: "creadoDate",
                table: "Categoria",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creadoDate",
                table: "Categoria");

            migrationBuilder.AddColumn<string>(
                name: "creadopor",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
