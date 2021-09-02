using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{ // ReSharper disable once InconsistentNaming
    public partial class addSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "SettingsId",
                "Companies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                "CompanySettings",
                table => new
                {
                    SettingsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Uid = table.Column<string>(nullable: false),
                    StartingInvoiceNr = table.Column<int>(nullable: false),
                    LogoUri = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    WebsiteUri = table.Column<string>(nullable: true),
                    Iban = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_CompanySettings", x => x.SettingsId); });

            migrationBuilder.CreateIndex(
                "IX_Companies_SettingsId",
                "Companies",
                "SettingsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Companies_CompanySettings_SettingsId",
                "Companies",
                "SettingsId",
                "CompanySettings",
                principalColumn: "SettingsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Companies_CompanySettings_SettingsId",
                "Companies");

            migrationBuilder.DropTable(
                "CompanySettings");

            migrationBuilder.DropIndex(
                "IX_Companies_SettingsId",
                "Companies");

            migrationBuilder.DropColumn(
                "SettingsId",
                "Companies");
        }
    }
}