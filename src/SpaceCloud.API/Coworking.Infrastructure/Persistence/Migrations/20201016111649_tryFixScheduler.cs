using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class tryFixScheduler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Schedulers_Rooms_RoomId",
                "Schedulers");

            migrationBuilder.AddForeignKey(
                "FK_Schedulers_Rooms_RoomId",
                "Schedulers",
                "RoomId",
                "Rooms",
                principalColumn: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Schedulers_Rooms_RoomId",
                "Schedulers");

            migrationBuilder.AddForeignKey(
                "FK_Schedulers_Rooms_RoomId",
                "Schedulers",
                "RoomId",
                "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}