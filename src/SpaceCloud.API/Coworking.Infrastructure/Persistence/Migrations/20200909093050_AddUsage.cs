using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class AddUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Usage",
                "RoomOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "Usage",
                "DiverseOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "Usage",
                "DeskOrders",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Usage",
                "RoomOrders");

            migrationBuilder.DropColumn(
                "Usage",
                "DiverseOrders");

            migrationBuilder.DropColumn(
                "Usage",
                "DeskOrders");
        }
    }
}