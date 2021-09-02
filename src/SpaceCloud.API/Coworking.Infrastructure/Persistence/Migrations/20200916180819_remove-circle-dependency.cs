using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    // ReSharper disable once InconsistentNaming
    public partial class removecircledependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Companies_Users_OwnerId",
                "Companies");

            migrationBuilder.DropIndex(
                "IX_Companies_OwnerId",
                "Companies");

            migrationBuilder.DropColumn(
                "RegisteredDate",
                "Users");

            migrationBuilder.DropColumn(
                "OwnerId",
                "Companies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "RegisteredDate",
                "Users",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                "OwnerId",
                "Companies",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Companies_OwnerId",
                "Companies",
                "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Companies_Users_OwnerId",
                "Companies",
                "OwnerId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}