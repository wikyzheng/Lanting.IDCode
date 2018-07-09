using Microsoft.EntityFrameworkCore.Migrations;

namespace Lanting.IDCode.Migrations
{
    public partial class user_custom_fileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowCodeCount",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllowProductCount",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowCodeCount",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "AllowProductCount",
                table: "AbpUsers");
        }
    }
}
