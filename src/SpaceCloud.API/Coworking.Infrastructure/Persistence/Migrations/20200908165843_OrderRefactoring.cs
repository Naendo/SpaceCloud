using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class OrderRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_DeskOrders_Desks_DeskId",
                "DeskOrders");

            migrationBuilder.DropForeignKey(
                "FK_DiverseOrders_Diverses_ProductId",
                "DiverseOrders");

            migrationBuilder.DropForeignKey(
                "FK_Invoices_DeskOrders_DeskOrderOrderId",
                "Invoices");

            migrationBuilder.DropForeignKey(
                "FK_RoomOrders_Rooms_RoomId",
                "RoomOrders");

            migrationBuilder.DropIndex(
                "IX_RoomOrders_RoomId",
                "RoomOrders");

            migrationBuilder.DropIndex(
                "IX_Invoices_DeskOrderOrderId",
                "Invoices");

            migrationBuilder.DropIndex(
                "IX_DiverseOrders_ProductId",
                "DiverseOrders");

            migrationBuilder.DropIndex(
                "IX_DeskOrders_DeskId",
                "DeskOrders");

            migrationBuilder.DropColumn(
                "Duration",
                "RoomOrders");

            migrationBuilder.DropColumn(
                "RoomId",
                "RoomOrders");

            migrationBuilder.DropColumn(
                "DeskOrderOrderId",
                "Invoices");

            migrationBuilder.DropColumn(
                "IsOpen",
                "Invoices");

            migrationBuilder.DropColumn(
                "ProductId",
                "DiverseOrders");

            migrationBuilder.DropColumn(
                "DeskId",
                "DeskOrders");

            migrationBuilder.DropColumn(
                "Duration",
                "DeskOrders");

            migrationBuilder.AddColumn<int>(
                "Pid",
                "RoomOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                "PdfUri",
                "Invoices",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                "DeskOrderId",
                "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "InvoiceNr",
                "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Pid",
                "DiverseOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Pid",
                "DeskOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_RoomOrders_Pid",
                "RoomOrders",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_Invoices_DeskOrderId",
                "Invoices",
                "DeskOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_DiverseOrders_Pid",
                "DiverseOrders",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_DeskOrders_Pid",
                "DeskOrders",
                "Pid");

            migrationBuilder.AddForeignKey(
                "FK_DeskOrders_Desks_Pid",
                "DeskOrders",
                "Pid",
                "Desks",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_DiverseOrders_Diverses_Pid",
                "DiverseOrders",
                "Pid",
                "Diverses",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_DeskOrders_DeskOrderId",
                "Invoices",
                "DeskOrderId",
                "DeskOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_RoomOrders_Rooms_Pid",
                "RoomOrders",
                "Pid",
                "Rooms",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_DeskOrders_Desks_Pid",
                "DeskOrders");

            migrationBuilder.DropForeignKey(
                "FK_DiverseOrders_Diverses_Pid",
                "DiverseOrders");

            migrationBuilder.DropForeignKey(
                "FK_Invoices_DeskOrders_DeskOrderId",
                "Invoices");

            migrationBuilder.DropForeignKey(
                "FK_RoomOrders_Rooms_Pid",
                "RoomOrders");

            migrationBuilder.DropIndex(
                "IX_RoomOrders_Pid",
                "RoomOrders");

            migrationBuilder.DropIndex(
                "IX_Invoices_DeskOrderId",
                "Invoices");

            migrationBuilder.DropIndex(
                "IX_DiverseOrders_Pid",
                "DiverseOrders");

            migrationBuilder.DropIndex(
                "IX_DeskOrders_Pid",
                "DeskOrders");

            migrationBuilder.DropColumn(
                "Pid",
                "RoomOrders");

            migrationBuilder.DropColumn(
                "DeskOrderId",
                "Invoices");

            migrationBuilder.DropColumn(
                "InvoiceNr",
                "Invoices");

            migrationBuilder.DropColumn(
                "Pid",
                "DiverseOrders");

            migrationBuilder.DropColumn(
                "Pid",
                "DeskOrders");

            migrationBuilder.AddColumn<int>(
                "Duration",
                "RoomOrders",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "RoomId",
                "RoomOrders",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                "PdfUri",
                "Invoices",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "DeskOrderOrderId",
                "Invoices",
                "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsOpen",
                "Invoices",
                "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                "ProductId",
                "DiverseOrders",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "DeskId",
                "DeskOrders",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Duration",
                "DeskOrders",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_RoomOrders_RoomId",
                "RoomOrders",
                "RoomId");

            migrationBuilder.CreateIndex(
                "IX_Invoices_DeskOrderOrderId",
                "Invoices",
                "DeskOrderOrderId");

            migrationBuilder.CreateIndex(
                "IX_DiverseOrders_ProductId",
                "DiverseOrders",
                "ProductId");

            migrationBuilder.CreateIndex(
                "IX_DeskOrders_DeskId",
                "DeskOrders",
                "DeskId");

            migrationBuilder.AddForeignKey(
                "FK_DeskOrders_Desks_DeskId",
                "DeskOrders",
                "DeskId",
                "Desks",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_DiverseOrders_Diverses_ProductId",
                "DiverseOrders",
                "ProductId",
                "Diverses",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_DeskOrders_DeskOrderOrderId",
                "Invoices",
                "DeskOrderOrderId",
                "DeskOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_RoomOrders_Rooms_RoomId",
                "RoomOrders",
                "RoomId",
                "Rooms",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}