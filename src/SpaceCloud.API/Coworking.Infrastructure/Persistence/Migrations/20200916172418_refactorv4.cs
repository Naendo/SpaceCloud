using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    // ReSharper disable once InconsistentNaming
    public partial class refactorv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Desks_CompanyLocations_LocationId",
                "Desks");

            migrationBuilder.DropForeignKey(
                "FK_Desks_Users_ModifiedByUserId",
                "Desks");

            migrationBuilder.DropForeignKey(
                "FK_Invoices_DeskOrders_DeskOrderId",
                "Invoices");

            migrationBuilder.DropForeignKey(
                "FK_Invoices_DiverseOrders_DiverseOrderId",
                "Invoices");

            migrationBuilder.DropForeignKey(
                "FK_Invoices_RoomOrders_RoomOrderId",
                "Invoices");

            migrationBuilder.DropForeignKey(
                "FK_Invoices_Users_UserId",
                "Invoices");

            migrationBuilder.DropForeignKey(
                "FK_Rooms_CompanyLocations_LocationId",
                "Rooms");

            migrationBuilder.DropForeignKey(
                "FK_Rooms_Users_ModifiedByUserId",
                "Rooms");

            migrationBuilder.DropForeignKey(
                "FK_Users_Companies_CompanyId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_CompanyLocations_CompanyLocationId",
                "Users");

            migrationBuilder.DropTable(
                "BillingAdress");

            migrationBuilder.DropTable(
                "DeskOrders");

            migrationBuilder.DropTable(
                "DiverseOrders");

            migrationBuilder.DropTable(
                "OperationTime");

            migrationBuilder.DropTable(
                "RecentActions");

            migrationBuilder.DropTable(
                "RoomOrders");

            migrationBuilder.DropTable(
                "UserIdentity");

            migrationBuilder.DropTable(
                "Diverses");

            migrationBuilder.DropIndex(
                "IX_Users_CompanyId",
                "Users");

            migrationBuilder.DropIndex(
                "IX_Users_CompanyLocationId",
                "Users");

            migrationBuilder.DropPrimaryKey(
                "PK_Rooms",
                "Rooms");

            migrationBuilder.DropIndex(
                "IX_Rooms_LocationId",
                "Rooms");

            migrationBuilder.DropIndex(
                "IX_Rooms_ModifiedByUserId",
                "Rooms");

            migrationBuilder.DropIndex(
                "IX_Invoices_DeskOrderId",
                "Invoices");

            migrationBuilder.DropIndex(
                "IX_Invoices_DiverseOrderId",
                "Invoices");

            migrationBuilder.DropIndex(
                "IX_Invoices_RoomOrderId",
                "Invoices");

            migrationBuilder.DropIndex(
                "IX_Invoices_UserId",
                "Invoices");

            migrationBuilder.DropPrimaryKey(
                "PK_Desks",
                "Desks");

            migrationBuilder.DropIndex(
                "IX_Desks_LocationId",
                "Desks");

            migrationBuilder.DropIndex(
                "IX_Desks_ModifiedByUserId",
                "Desks");

            migrationBuilder.DropColumn(
                "CompanyId",
                "Users");

            migrationBuilder.DropColumn(
                "CompanyLocationId",
                "Users");

            migrationBuilder.DropColumn(
                "CreationDate",
                "Users");

            migrationBuilder.DropColumn(
                "IsPermittedUser",
                "Users");

            migrationBuilder.DropColumn(
                "LastLogged",
                "Users");

            migrationBuilder.DropColumn(
                "CreationDate",
                "Tickets");

            migrationBuilder.DropColumn(
                "Pid",
                "Rooms");

            migrationBuilder.DropColumn(
                "Description",
                "Rooms");

            migrationBuilder.DropColumn(
                "ImageUri",
                "Rooms");

            migrationBuilder.DropColumn(
                "IsEnabled",
                "Rooms");

            migrationBuilder.DropColumn(
                "LocationId",
                "Rooms");

            migrationBuilder.DropColumn(
                "ModifiedByUserId",
                "Rooms");

            migrationBuilder.DropColumn(
                "Name",
                "Rooms");

            migrationBuilder.DropColumn(
                "PricePerHour",
                "Rooms");

            migrationBuilder.DropColumn(
                "CreationDate",
                "Invoices");

            migrationBuilder.DropColumn(
                "DeskOrderId",
                "Invoices");

            migrationBuilder.DropColumn(
                "DiverseOrderId",
                "Invoices");

            migrationBuilder.DropColumn(
                "RoomOrderId",
                "Invoices");

            migrationBuilder.DropColumn(
                "UserId",
                "Invoices");

            migrationBuilder.DropColumn(
                "Pid",
                "Desks");

            migrationBuilder.DropColumn(
                "AmountOfDesks",
                "Desks");

            migrationBuilder.DropColumn(
                "Description",
                "Desks");

            migrationBuilder.DropColumn(
                "ImageUri",
                "Desks");

            migrationBuilder.DropColumn(
                "IsEnabled",
                "Desks");

            migrationBuilder.DropColumn(
                "LocationId",
                "Desks");

            migrationBuilder.DropColumn(
                "ModifiedByUserId",
                "Desks");

            migrationBuilder.DropColumn(
                "Name",
                "Desks");

            migrationBuilder.DropColumn(
                "PricePerMonth",
                "Desks");

            migrationBuilder.DropColumn(
                "Adress",
                "CompanyLocations");

            migrationBuilder.DropColumn(
                "JoinedAt",
                "Companies");

            migrationBuilder.RenameColumn(
                "ZIP",
                "CompanyLocations",
                "Zip");

            migrationBuilder.AddColumn<int>(
                "BillingId",
                "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "IdentityId",
                "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "LocationId",
                "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                "RegisteredDate",
                "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                    "RoomId",
                    "Rooms",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                "ProductId",
                "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                "PdfUri",
                "Invoices",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                "InvoiceNr",
                "Invoices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "OrderId",
                "Invoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                    "DeskId",
                    "Desks",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                "AvailableProductAmount",
                "Desks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "IntervalType",
                "Desks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "ProductId",
                "Desks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "Address",
                "CompanyLocations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                "OwnerId",
                "Companies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                "PK_Rooms",
                "Rooms",
                "RoomId");

            migrationBuilder.AddPrimaryKey(
                "PK_Desks",
                "Desks",
                "DeskId");

            migrationBuilder.CreateTable(
                "BillingAddresses",
                table => new
                {
                    BillingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Floor = table.Column<string>(nullable: true),
                    Door = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_BillingAddresses", x => x.BillingId); });

            migrationBuilder.CreateTable(
                "Identities",
                table => new
                {
                    IdentityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastLogged = table.Column<DateTime>(nullable: false),
                    Hash = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    StayLogged = table.Column<bool>(nullable: false),
                    PasswordResetToken = table.Column<string>(nullable: true),
                    PasswordResetTokenExpires = table.Column<DateTime>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    RefreshTokenExpires = table.Column<DateTime>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Identities", x => x.IdentityId); });

            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    Usage = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_Orders_Invoices_OrderId",
                        x => x.OrderId,
                        "Invoices",
                        "InvoiceId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        "FK_Orders_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                "Products",
                table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ImageUri = table.Column<string>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    LastModifiedByUserId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        "FK_Products_Users_LastModifiedByUserId",
                        x => x.LastModifiedByUserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        "FK_Products_CompanyLocations_LocationId",
                        x => x.LocationId,
                        "CompanyLocations",
                        "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "RecentEvents",
                table => new
                {
                    ActionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Action = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentEvents", x => x.ActionId);
                    table.ForeignKey(
                        "FK_RecentEvents_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId");
                });

            migrationBuilder.CreateTable(
                "RoomTags",
                table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTags", x => x.RoomId);
                    table.ForeignKey(
                        "FK_RoomTags_Rooms_RoomId",
                        x => x.RoomId,
                        "Rooms",
                        "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Carts",
                table => new
                {
                    CartId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        "FK_Carts_Orders_CartId",
                        x => x.CartId,
                        "Orders",
                        "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Carts_Products_CartId",
                        x => x.CartId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Users_BillingId",
                "Users",
                "BillingId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Users_IdentityId",
                "Users",
                "IdentityId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Users_LocationId",
                "Users",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Rooms_ProductId",
                "Rooms",
                "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Desks_ProductId",
                "Desks",
                "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Companies_OwnerId",
                "Companies",
                "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Orders_UserId",
                "Orders",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Products_LastModifiedByUserId",
                "Products",
                "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_Products_LocationId",
                "Products",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_RecentEvents_UserId",
                "RecentEvents",
                "UserId");

            migrationBuilder.AddForeignKey(
                "FK_Companies_Users_OwnerId",
                "Companies",
                "OwnerId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Desks_Products_ProductId",
                "Desks",
                "ProductId",
                "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Rooms_Products_ProductId",
                "Rooms",
                "ProductId",
                "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_BillingAddresses_BillingId",
                "Users",
                "BillingId",
                "BillingAddresses",
                principalColumn: "BillingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_Identities_IdentityId",
                "Users",
                "IdentityId",
                "Identities",
                principalColumn: "IdentityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_CompanyLocations_LocationId",
                "Users",
                "LocationId",
                "CompanyLocations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Companies_Users_OwnerId",
                "Companies");

            migrationBuilder.DropForeignKey(
                "FK_Desks_Products_ProductId",
                "Desks");

            migrationBuilder.DropForeignKey(
                "FK_Rooms_Products_ProductId",
                "Rooms");

            migrationBuilder.DropForeignKey(
                "FK_Users_BillingAddresses_BillingId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_Identities_IdentityId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_CompanyLocations_LocationId",
                "Users");

            migrationBuilder.DropTable(
                "BillingAddresses");

            migrationBuilder.DropTable(
                "Carts");

            migrationBuilder.DropTable(
                "Identities");

            migrationBuilder.DropTable(
                "RecentEvents");

            migrationBuilder.DropTable(
                "RoomTags");

            migrationBuilder.DropTable(
                "Orders");

            migrationBuilder.DropTable(
                "Products");

            migrationBuilder.DropIndex(
                "IX_Users_BillingId",
                "Users");

            migrationBuilder.DropIndex(
                "IX_Users_IdentityId",
                "Users");

            migrationBuilder.DropIndex(
                "IX_Users_LocationId",
                "Users");

            migrationBuilder.DropPrimaryKey(
                "PK_Rooms",
                "Rooms");

            migrationBuilder.DropIndex(
                "IX_Rooms_ProductId",
                "Rooms");

            migrationBuilder.DropPrimaryKey(
                "PK_Desks",
                "Desks");

            migrationBuilder.DropIndex(
                "IX_Desks_ProductId",
                "Desks");

            migrationBuilder.DropIndex(
                "IX_Companies_OwnerId",
                "Companies");

            migrationBuilder.DropColumn(
                "BillingId",
                "Users");

            migrationBuilder.DropColumn(
                "IdentityId",
                "Users");

            migrationBuilder.DropColumn(
                "LocationId",
                "Users");

            migrationBuilder.DropColumn(
                "RegisteredDate",
                "Users");

            migrationBuilder.DropColumn(
                "RoomId",
                "Rooms");

            migrationBuilder.DropColumn(
                "ProductId",
                "Rooms");

            migrationBuilder.DropColumn(
                "OrderId",
                "Invoices");

            migrationBuilder.DropColumn(
                "DeskId",
                "Desks");

            migrationBuilder.DropColumn(
                "AvailableProductAmount",
                "Desks");

            migrationBuilder.DropColumn(
                "IntervalType",
                "Desks");

            migrationBuilder.DropColumn(
                "ProductId",
                "Desks");

            migrationBuilder.DropColumn(
                "Address",
                "CompanyLocations");

            migrationBuilder.DropColumn(
                "OwnerId",
                "Companies");

            migrationBuilder.RenameColumn(
                "Zip",
                "CompanyLocations",
                "ZIP");

            migrationBuilder.AddColumn<int>(
                "CompanyId",
                "Users",
                "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "CompanyLocationId",
                "Users",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                "CreationDate",
                "Users",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                "IsPermittedUser",
                "Users",
                "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                "LastLogged",
                "Users",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreationDate",
                "Tickets",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                    "Pid",
                    "Rooms",
                    "integer",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                "Description",
                "Rooms",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "ImageUri",
                "Rooms",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                "IsEnabled",
                "Rooms",
                "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                "LocationId",
                "Rooms",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "ModifiedByUserId",
                "Rooms",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "Name",
                "Rooms",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                "PricePerHour",
                "Rooms",
                "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                "PdfUri",
                "Invoices",
                "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                "InvoiceNr",
                "Invoices",
                "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                "CreationDate",
                "Invoices",
                "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "DeskOrderId",
                "Invoices",
                "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "DiverseOrderId",
                "Invoices",
                "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "RoomOrderId",
                "Invoices",
                "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "UserId",
                "Invoices",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                    "Pid",
                    "Desks",
                    "integer",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                "AmountOfDesks",
                "Desks",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "Description",
                "Desks",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "ImageUri",
                "Desks",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                "IsEnabled",
                "Desks",
                "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                "LocationId",
                "Desks",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "ModifiedByUserId",
                "Desks",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "Name",
                "Desks",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                "PricePerMonth",
                "Desks",
                "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                "Adress",
                "CompanyLocations",
                "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                "JoinedAt",
                "Companies",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                "PK_Rooms",
                "Rooms",
                "Pid");

            migrationBuilder.AddPrimaryKey(
                "PK_Desks",
                "Desks",
                "Pid");

            migrationBuilder.CreateTable(
                "BillingAdress",
                table => new
                {
                    BId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adress = table.Column<string>("text", nullable: false),
                    City = table.Column<string>("text", nullable: false),
                    Country = table.Column<string>("text", nullable: false),
                    Door = table.Column<string>("text", nullable: true),
                    Floor = table.Column<string>("text", nullable: true),
                    UserId = table.Column<int>("integer", nullable: false),
                    ZIP = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAdress", x => x.BId);
                    table.ForeignKey(
                        "FK_BillingAdress_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "DeskOrders",
                table => new
                {
                    OrderId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Pid = table.Column<int>("integer", nullable: false),
                    StartDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Usage = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskOrders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_DeskOrders_Desks_Pid",
                        x => x.Pid,
                        "Desks",
                        "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Diverses",
                table => new
                {
                    Pid = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AmountOfDesks = table.Column<int>("integer", nullable: false),
                    Description = table.Column<string>("text", nullable: false),
                    ImageUri = table.Column<string>("text", nullable: false),
                    IsEnabled = table.Column<bool>("boolean", nullable: false),
                    LocationId = table.Column<int>("integer", nullable: false),
                    ModifiedByUserId = table.Column<int>("integer", nullable: false),
                    Name = table.Column<string>("text", nullable: false),
                    PricePerMonth = table.Column<decimal>("numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diverses", x => x.Pid);
                    table.ForeignKey(
                        "FK_Diverses_CompanyLocations_LocationId",
                        x => x.LocationId,
                        "CompanyLocations",
                        "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Diverses_Users_ModifiedByUserId",
                        x => x.ModifiedByUserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "OperationTime",
                table => new
                {
                    OtId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Closing = table.Column<string>("text", nullable: false),
                    Day = table.Column<string>("text", nullable: false),
                    Opening = table.Column<string>("text", nullable: false),
                    Pid = table.Column<int>("integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTime", x => x.OtId);
                    table.ForeignKey(
                        "FK_OperationTime_Rooms_Pid",
                        x => x.Pid,
                        "Rooms",
                        "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "RecentActions",
                table => new
                {
                    ActionId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Action = table.Column<string>("text", nullable: false),
                    CompanyId = table.Column<int>("integer", nullable: false),
                    Date = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    FirstName = table.Column<string>("text", nullable: false),
                    LastName = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_RecentActions", x => x.ActionId); });

            migrationBuilder.CreateTable(
                "RoomOrders",
                table => new
                {
                    OrderId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Pid = table.Column<int>("integer", nullable: false),
                    StartDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Usage = table.Column<string>("text", nullable: false),
                    UserId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOrders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_RoomOrders_Rooms_Pid",
                        x => x.Pid,
                        "Rooms",
                        "Pid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_RoomOrders_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "UserIdentity",
                table => new
                {
                    AuthId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mail = table.Column<string>("text", nullable: false),
                    Password = table.Column<string>("text", nullable: false),
                    RefreshToken = table.Column<string>("text", nullable: true),
                    RefreshTokenExpires = table.Column<DateTime>("timestamp without time zone", nullable: true),
                    ResetToken = table.Column<string>("text", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>("timestamp without time zone", nullable: true),
                    Role = table.Column<int>("integer", nullable: false),
                    Salt = table.Column<string>("text", nullable: false),
                    StayLogged = table.Column<bool>("boolean", nullable: false),
                    UserId = table.Column<int>("integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIdentity", x => x.AuthId);
                    table.ForeignKey(
                        "FK_UserIdentity_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "DiverseOrders",
                table => new
                {
                    OrderId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Pid = table.Column<int>("integer", nullable: false),
                    StartDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Usage = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiverseOrders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_DiverseOrders_Diverses_Pid",
                        x => x.Pid,
                        "Diverses",
                        "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Users_CompanyId",
                "Users",
                "CompanyId");

            migrationBuilder.CreateIndex(
                "IX_Users_CompanyLocationId",
                "Users",
                "CompanyLocationId");

            migrationBuilder.CreateIndex(
                "IX_Rooms_LocationId",
                "Rooms",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Rooms_ModifiedByUserId",
                "Rooms",
                "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_Invoices_DeskOrderId",
                "Invoices",
                "DeskOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Invoices_DiverseOrderId",
                "Invoices",
                "DiverseOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Invoices_RoomOrderId",
                "Invoices",
                "RoomOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Invoices_UserId",
                "Invoices",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Desks_LocationId",
                "Desks",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Desks_ModifiedByUserId",
                "Desks",
                "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_BillingAdress_UserId",
                "BillingAdress",
                "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_DeskOrders_Pid",
                "DeskOrders",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_DiverseOrders_Pid",
                "DiverseOrders",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_Diverses_LocationId",
                "Diverses",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Diverses_ModifiedByUserId",
                "Diverses",
                "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_OperationTime_Pid",
                "OperationTime",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_RoomOrders_Pid",
                "RoomOrders",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_RoomOrders_UserId",
                "RoomOrders",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserIdentity_UserId",
                "UserIdentity",
                "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Desks_CompanyLocations_LocationId",
                "Desks",
                "LocationId",
                "CompanyLocations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Desks_Users_ModifiedByUserId",
                "Desks",
                "ModifiedByUserId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_DeskOrders_DeskOrderId",
                "Invoices",
                "DeskOrderId",
                "DeskOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_DiverseOrders_DiverseOrderId",
                "Invoices",
                "DiverseOrderId",
                "DiverseOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_RoomOrders_RoomOrderId",
                "Invoices",
                "RoomOrderId",
                "RoomOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Invoices_Users_UserId",
                "Invoices",
                "UserId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Rooms_CompanyLocations_LocationId",
                "Rooms",
                "LocationId",
                "CompanyLocations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Rooms_Users_ModifiedByUserId",
                "Rooms",
                "ModifiedByUserId",
                "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_Companies_CompanyId",
                "Users",
                "CompanyId",
                "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Users_CompanyLocations_CompanyLocationId",
                "Users",
                "CompanyLocationId",
                "CompanyLocations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}