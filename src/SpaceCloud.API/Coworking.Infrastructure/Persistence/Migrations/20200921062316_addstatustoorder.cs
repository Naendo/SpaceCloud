using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class addstatustoorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsAccepted",
                "Invoices");

            migrationBuilder.DropColumn(
                "IsDeclined",
                "Invoices");

            migrationBuilder.DropColumn(
                "IsPayed",
                "Invoices");

            migrationBuilder.AddColumn<bool>(
                "IsAccepted",
                "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsDeclined",
                "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsPayed",
                "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsAccepted",
                "Orders");

            migrationBuilder.DropColumn(
                "IsDeclined",
                "Orders");

            migrationBuilder.DropColumn(
                "IsPayed",
                "Orders");

            migrationBuilder.AddColumn<bool>(
                "IsAccepted",
                "Invoices",
                "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsDeclined",
                "Invoices",
                "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsPayed",
                "Invoices",
                "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}