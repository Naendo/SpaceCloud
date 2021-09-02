using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class updateProductDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Carts_Products_ProductId",
                "Carts");

            migrationBuilder.DropColumn(
                "ContactPhone",
                "CompanySettings");

            migrationBuilder.DropColumn(
                "IBAN",
                "CompanySettings");

            migrationBuilder.AddForeignKey(
                "FK_Carts_Products_ProductId",
                "Carts",
                "ProductId",
                "Products",
                principalColumn: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Carts_Products_ProductId",
                "Carts");

            migrationBuilder.AddColumn<string>(
                "ContactPhone",
                "CompanySettings",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "IBAN",
                "CompanySettings",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                "FK_Carts_Products_ProductId",
                "Carts",
                "ProductId",
                "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}