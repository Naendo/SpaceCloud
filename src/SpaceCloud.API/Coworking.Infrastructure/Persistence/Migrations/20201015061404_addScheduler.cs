using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class addScheduler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Schedulers",
                table => new
                {
                    SchedulerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastLogged = table.Column<DateTime>(nullable: true),
                    ActivatorToken = table.Column<string>(nullable: false),
                    ReferenceToken = table.Column<string>(nullable: true),
                    TokenExpires = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulers", x => x.SchedulerId);
                    table.ForeignKey(
                        "FK_Schedulers_Rooms_RoomId",
                        x => x.RoomId,
                        "Rooms",
                        "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Schedulers_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Schedulers_RoomId",
                "Schedulers",
                "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Schedulers_UserId",
                "Schedulers",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Schedulers");
        }
    }
}