using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lanting.IDCode.Migrations
{
    public partial class userallowcodecount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_generate_task_product_info_ProductId",
                table: "generate_task");

            migrationBuilder.DropIndex(
                name: "IX_generate_task_ProductId",
                table: "generate_task");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "identity_code",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TotalCountCount",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCountCount",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "identity_code",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_generate_task_ProductId",
                table: "generate_task",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_generate_task_product_info_ProductId",
                table: "generate_task",
                column: "ProductId",
                principalTable: "product_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
