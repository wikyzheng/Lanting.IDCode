using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lanting.IDCode.Migrations
{
    public partial class myentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "generate_task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: false),
                    FailReason = table.Column<string>(nullable: true),
                    Completed = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    GenerateCount = table.Column<int>(nullable: false),
                    TaskStatu = table.Column<int>(nullable: false),
                    DataFilePath = table.Column<string>(nullable: true),
                    IsAntiFake = table.Column<bool>(nullable: false),
                    AFCodeLength = table.Column<int>(maxLength: 8, nullable: true),
                    AntiFackCodeType = table.Column<int>(nullable: false),
                    AntiFackCode = table.Column<string>(nullable: true),
                    StartOne = table.Column<long>(nullable: false),
                    EndOne = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generate_task", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "identity_code",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    AntiFakeCode = table.Column<string>(nullable: true),
                    IsActived = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_info", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "generate_task");

            migrationBuilder.DropTable(
                name: "identity_code");

            migrationBuilder.DropTable(
                name: "product_info");

           
        }
    }
}
