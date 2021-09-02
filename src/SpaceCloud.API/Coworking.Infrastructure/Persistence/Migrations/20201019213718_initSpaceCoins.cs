using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class initSpaceCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "CurrencyAmountPerSubscription",
                "CompanySettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                "SpaceCoin",
                table => new
                {
                    CoinId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Hash = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CoinGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceCoin", x => x.CoinId);
                    table.ForeignKey(
                        "FK_SpaceCoin_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_SpaceCoin_UserId",
                "SpaceCoin",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SpaceCoin");

            migrationBuilder.DropColumn(
                "CurrencyAmountPerSubscription",
                "CompanySettings");
        }
    }
}