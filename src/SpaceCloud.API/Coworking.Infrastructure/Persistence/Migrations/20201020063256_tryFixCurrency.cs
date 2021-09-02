using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class tryFixCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_SpaceCoin_Users_UserId",
                "SpaceCoin");

            migrationBuilder.DropPrimaryKey(
                "PK_SpaceCoin",
                "SpaceCoin");

            migrationBuilder.RenameTable(
                "SpaceCoin",
                newName: "SpaceCoins");

            migrationBuilder.RenameIndex(
                "IX_SpaceCoin_UserId",
                table: "SpaceCoins",
                newName: "IX_SpaceCoins_UserId");

            migrationBuilder.AddPrimaryKey(
                "PK_SpaceCoins",
                "SpaceCoins",
                "CoinId");

            migrationBuilder.AddForeignKey(
                "FK_SpaceCoins_Users_UserId",
                "SpaceCoins",
                "UserId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_SpaceCoins_Users_UserId",
                "SpaceCoins");

            migrationBuilder.DropPrimaryKey(
                "PK_SpaceCoins",
                "SpaceCoins");

            migrationBuilder.RenameTable(
                "SpaceCoins",
                newName: "SpaceCoin");

            migrationBuilder.RenameIndex(
                "IX_SpaceCoins_UserId",
                table: "SpaceCoin",
                newName: "IX_SpaceCoin_UserId");

            migrationBuilder.AddPrimaryKey(
                "PK_SpaceCoin",
                "SpaceCoin",
                "CoinId");

            migrationBuilder.AddForeignKey(
                "FK_SpaceCoin_Users_UserId",
                "SpaceCoin",
                "UserId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}