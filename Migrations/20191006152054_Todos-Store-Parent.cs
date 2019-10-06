using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Motivator.Migrations
{
    public partial class TodosStoreParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTodoIds",
                table: "Todos");

            migrationBuilder.AddColumn<int>(
                name: "ParentTodoId",
                table: "Todos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentTodoId",
                table: "Todos");

            migrationBuilder.AddColumn<List<int>>(
                name: "SubTodoIds",
                table: "Todos",
                nullable: true);
        }
    }
}
