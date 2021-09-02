using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class fixaddedat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "RoomTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Rooms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "RecentEvents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Identities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Desks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "CompanyLocations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "Carts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedAt",
                "BillingAddresses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CreatedAt",
                "Users");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Tickets");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "RoomTags");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Rooms");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "RecentEvents");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Products");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Orders");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Invoices");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Identities");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Desks");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "CompanyLocations");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Companies");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "Carts");

            migrationBuilder.DropColumn(
                "CreatedAt",
                "BillingAddresses");
        }
    }
}