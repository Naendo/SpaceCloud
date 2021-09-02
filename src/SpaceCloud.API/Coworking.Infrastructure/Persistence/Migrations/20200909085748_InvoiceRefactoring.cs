using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class InvoiceRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsDenied",
                "Invoices");

            migrationBuilder.AlterColumn<DateTime>(
                "CreationDate",
                "Invoices",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<bool>(
                "IsDeclined",
                "Invoices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsDeclined",
                "Invoices");

            migrationBuilder.AlterColumn<DateTime>(
                "CreationDate",
                "Invoices",
                "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsDenied",
                "Invoices",
                "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}