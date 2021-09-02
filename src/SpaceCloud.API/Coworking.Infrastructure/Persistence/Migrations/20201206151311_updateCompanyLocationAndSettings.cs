using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class updateCompanyLocationAndSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CoinGuid",
                "SpaceCoins");

            migrationBuilder.AddColumn<string>(
                "BIC",
                "CompanySettings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "BankName",
                "CompanySettings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "ContactMail",
                "CompanySettings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "ContactPhone",
                "CompanySettings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "IBAN",
                "CompanySettings",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "BIC",
                "CompanySettings");

            migrationBuilder.DropColumn(
                "BankName",
                "CompanySettings");

            migrationBuilder.DropColumn(
                "ContactMail",
                "CompanySettings");

            migrationBuilder.DropColumn(
                "ContactPhone",
                "CompanySettings");

            migrationBuilder.DropColumn(
                "IBAN",
                "CompanySettings");

            migrationBuilder.AddColumn<Guid>(
                "CoinGuid",
                "SpaceCoins",
                "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}