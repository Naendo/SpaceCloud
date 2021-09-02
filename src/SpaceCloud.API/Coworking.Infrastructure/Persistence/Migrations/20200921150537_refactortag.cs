using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class refactortag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_RoomTags",
                "RoomTags");

            migrationBuilder.AlterColumn<int>(
                    "TagId",
                    "RoomTags",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                "PK_RoomTags",
                "RoomTags",
                "TagId");

            migrationBuilder.CreateIndex(
                "IX_RoomTags_RoomId",
                "RoomTags",
                "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_RoomTags",
                "RoomTags");

            migrationBuilder.DropIndex(
                "IX_RoomTags_RoomId",
                "RoomTags");

            migrationBuilder.AlterColumn<int>(
                    "TagId",
                    "RoomTags",
                    "integer",
                    nullable: false,
                    oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                "PK_RoomTags",
                "RoomTags",
                "RoomId");
        }
    }
}