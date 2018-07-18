using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lanting.IDCode.Migrations
{
    public partial class identity_code_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntiFackCode",
                table: "generate_task");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "generate_task");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AntiFackCode",
                table: "generate_task",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Completed",
                table: "generate_task",
                nullable: true);
        }
    }
}
