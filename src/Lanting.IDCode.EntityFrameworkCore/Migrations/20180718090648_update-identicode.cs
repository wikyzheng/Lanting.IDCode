using Microsoft.EntityFrameworkCore.Migrations;

namespace Lanting.IDCode.Migrations
{
    public partial class updateidenticode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComfuseCode",
                table: "identity_code",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_generate_task_product_info_ProductId",
                table: "generate_task");

            migrationBuilder.DropIndex(
                name: "IX_generate_task_ProductId",
                table: "generate_task");

            migrationBuilder.DropColumn(
                name: "ComfuseCode",
                table: "identity_code");
        }
    }
}
