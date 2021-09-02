using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class addOrderNr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNr",
                table: "Orders",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNr",
                table: "Orders");
        }
    }
}
