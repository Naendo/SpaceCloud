using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class cartbugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Carts_Orders_CartId",
                "Carts");

            migrationBuilder.DropForeignKey(
                "FK_Carts_Products_CartId",
                "Carts");

            migrationBuilder.AlterColumn<int>(
                    "CartId",
                    "Carts",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                "IX_Carts_OrderId",
                "Carts",
                "OrderId");

            migrationBuilder.CreateIndex(
                "IX_Carts_ProductId",
                "Carts",
                "ProductId");

            migrationBuilder.AddForeignKey(
                "FK_Carts_Orders_OrderId",
                "Carts",
                "OrderId",
                "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Carts_Products_ProductId",
                "Carts",
                "ProductId",
                "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Carts_Orders_OrderId",
                "Carts");

            migrationBuilder.DropForeignKey(
                "FK_Carts_Products_ProductId",
                "Carts");

            migrationBuilder.DropIndex(
                "IX_Carts_OrderId",
                "Carts");

            migrationBuilder.DropIndex(
                "IX_Carts_ProductId",
                "Carts");

            migrationBuilder.AlterColumn<int>(
                    "CartId",
                    "Carts",
                    "integer",
                    nullable: false,
                    oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                "FK_Carts_Orders_CartId",
                "Carts",
                "CartId",
                "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Carts_Products_CartId",
                "Carts",
                "CartId",
                "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}