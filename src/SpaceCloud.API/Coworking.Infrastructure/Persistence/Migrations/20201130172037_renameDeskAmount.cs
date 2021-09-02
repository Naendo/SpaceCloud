using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class renameDeskAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "AvailableProductAmount",
                "Desks");

            migrationBuilder.AddColumn<int>(
                "ProductAmount",
                "Desks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ProductAmount",
                "Desks");

            migrationBuilder.AddColumn<int>(
                "AvailableProductAmount",
                "Desks",
                "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}