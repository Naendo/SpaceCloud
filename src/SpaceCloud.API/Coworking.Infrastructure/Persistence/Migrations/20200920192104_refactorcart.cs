using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class refactorcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "EndDate",
                "Orders");

            migrationBuilder.DropColumn(
                "StartDate",
                "Orders");

            migrationBuilder.DropColumn(
                "Usage",
                "Orders");

            migrationBuilder.AddColumn<DateTime>(
                "EndDate",
                "Carts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "StartDate",
                "Carts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                "Usage",
                "Carts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "EndDate",
                "Carts");

            migrationBuilder.DropColumn(
                "StartDate",
                "Carts");

            migrationBuilder.DropColumn(
                "Usage",
                "Carts");

            migrationBuilder.AddColumn<DateTime>(
                "EndDate",
                "Orders",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "StartDate",
                "Orders",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                "Usage",
                "Orders",
                "text",
                nullable: false,
                defaultValue: "");
        }
    }
}