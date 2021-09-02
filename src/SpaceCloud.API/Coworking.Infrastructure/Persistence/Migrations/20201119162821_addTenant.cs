using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class addTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "SubDomain",
                "Companies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                "IX_Companies_SubDomain",
                "Companies",
                "SubDomain",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Companies_SubDomain",
                "Companies");

            migrationBuilder.DropColumn(
                "SubDomain",
                "Companies");
        }
    }
}