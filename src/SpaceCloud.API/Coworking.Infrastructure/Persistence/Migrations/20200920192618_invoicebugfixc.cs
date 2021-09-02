using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class invoicebugfixc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Orders_Invoices_OrderId",
                "Orders");

            migrationBuilder.AlterColumn<int>(
                    "OrderId",
                    "Orders",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                "IX_Invoices_OrderId",
                "Invoices",
                "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_Orders_OrderId",
                "Invoices",
                "OrderId",
                "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Invoices_Orders_OrderId",
                "Invoices");

            migrationBuilder.DropIndex(
                "IX_Invoices_OrderId",
                "Invoices");

            migrationBuilder.AlterColumn<int>(
                    "OrderId",
                    "Orders",
                    "integer",
                    nullable: false,
                    oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                "FK_Orders_Invoices_OrderId",
                "Orders",
                "OrderId",
                "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}