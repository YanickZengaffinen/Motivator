using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Motivator.Migrations
{
    public partial class Completion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Todos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Todos");
        }
    }
}
