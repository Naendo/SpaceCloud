using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Companies",
                table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    JoinedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Companies", x => x.CompanyId); });

            migrationBuilder.CreateTable(
                "RecentActions",
                table => new
                {
                    ActionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Action = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_RecentActions", x => x.ActionId); });

            migrationBuilder.CreateTable(
                "CompanyLocations",
                table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ZIP = table.Column<string>(nullable: false),
                    Door = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocations", x => x.LocationId);
                    table.ForeignKey(
                        "FK_CompanyLocations_Companies_CompanyId",
                        x => x.CompanyId,
                        "Companies",
                        "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ImageUri = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastLogged = table.Column<DateTime>(nullable: false),
                    IsPermittedUser = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    CompanyLocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        "FK_Users_Companies_CompanyId",
                        x => x.CompanyId,
                        "Companies",
                        "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Users_CompanyLocations_CompanyLocationId",
                        x => x.CompanyLocationId,
                        "CompanyLocations",
                        "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "BillingAdress",
                table => new
                {
                    BId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ZIP = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Door = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
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
                "Desks",
                table => new
                {
                    Pid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: false),
                    ImageUri = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PricePerMonth = table.Column<decimal>(nullable: false),
                    AmountOfDesks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desks", x => x.Pid);
                    table.ForeignKey(
                        "FK_Desks_CompanyLocations_LocationId",
                        x => x.LocationId,
                        "CompanyLocations",
                        "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Desks_Users_ModifiedByUserId",
                        x => x.ModifiedByUserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Diverses",
                table => new
                {
                    Pid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: false),
                    ImageUri = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PricePerMonth = table.Column<decimal>(nullable: false),
                    AmountOfDesks = table.Column<int>(nullable: false)
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
                "Rooms",
                table => new
                {
                    Pid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: false),
                    ImageUri = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PricePerHour = table.Column<decimal>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Pid);
                    table.ForeignKey(
                        "FK_Rooms_CompanyLocations_LocationId",
                        x => x.LocationId,
                        "CompanyLocations",
                        "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Rooms_Users_ModifiedByUserId",
                        x => x.ModifiedByUserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Tickets",
                table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        "FK_Tickets_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserIdentity",
                table => new
                {
                    AuthId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    ResetToken = table.Column<string>(nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(nullable: true),
                    Mail = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    StayLogged = table.Column<bool>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    RefreshTokenExpires = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
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
                "DeskOrders",
                table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Duration = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    DeskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskOrders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_DeskOrders_Desks_DeskId",
                        x => x.DeskId,
                        "Desks",
                        "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "DiverseOrders",
                table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiverseOrders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_DiverseOrders_Diverses_ProductId",
                        x => x.ProductId,
                        "Diverses",
                        "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "OperationTime",
                table => new
                {
                    OtId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<string>(nullable: false),
                    Opening = table.Column<string>(nullable: false),
                    Closing = table.Column<string>(nullable: false),
                    Pid = table.Column<int>(nullable: false)
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
                "RoomOrders",
                table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Duration = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOrders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_RoomOrders_Rooms_RoomId",
                        x => x.RoomId,
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
                "Invoices",
                table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsOpen = table.Column<bool>(nullable: false),
                    PdfUri = table.Column<string>(nullable: false),
                    IsDenied = table.Column<bool>(nullable: false),
                    IsPayed = table.Column<bool>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeskOrderOrderId = table.Column<int>(nullable: true),
                    RoomOrderId = table.Column<int>(nullable: true),
                    DiverseOrderId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        "FK_Invoices_DeskOrders_DeskOrderOrderId",
                        x => x.DeskOrderOrderId,
                        "DeskOrders",
                        "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Invoices_DiverseOrders_DiverseOrderId",
                        x => x.DiverseOrderId,
                        "DiverseOrders",
                        "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Invoices_RoomOrders_RoomOrderId",
                        x => x.RoomOrderId,
                        "RoomOrders",
                        "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Invoices_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_BillingAdress_UserId",
                "BillingAdress",
                "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_CompanyLocations_CompanyId",
                "CompanyLocations",
                "CompanyId");

            migrationBuilder.CreateIndex(
                "IX_DeskOrders_DeskId",
                "DeskOrders",
                "DeskId");

            migrationBuilder.CreateIndex(
                "IX_Desks_LocationId",
                "Desks",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Desks_ModifiedByUserId",
                "Desks",
                "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_DiverseOrders_ProductId",
                "DiverseOrders",
                "ProductId");

            migrationBuilder.CreateIndex(
                "IX_Diverses_LocationId",
                "Diverses",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Diverses_ModifiedByUserId",
                "Diverses",
                "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_Invoices_DeskOrderOrderId",
                "Invoices",
                "DeskOrderOrderId");

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
                "IX_OperationTime_Pid",
                "OperationTime",
                "Pid");

            migrationBuilder.CreateIndex(
                "IX_RoomOrders_RoomId",
                "RoomOrders",
                "RoomId");

            migrationBuilder.CreateIndex(
                "IX_RoomOrders_UserId",
                "RoomOrders",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Rooms_LocationId",
                "Rooms",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_Rooms_ModifiedByUserId",
                "Rooms",
                "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                "IX_Tickets_UserId",
                "Tickets",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserIdentity_UserId",
                "UserIdentity",
                "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Users_CompanyId",
                "Users",
                "CompanyId");

            migrationBuilder.CreateIndex(
                "IX_Users_CompanyLocationId",
                "Users",
                "CompanyLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BillingAdress");

            migrationBuilder.DropTable(
                "Invoices");

            migrationBuilder.DropTable(
                "OperationTime");

            migrationBuilder.DropTable(
                "RecentActions");

            migrationBuilder.DropTable(
                "Tickets");

            migrationBuilder.DropTable(
                "UserIdentity");

            migrationBuilder.DropTable(
                "DeskOrders");

            migrationBuilder.DropTable(
                "DiverseOrders");

            migrationBuilder.DropTable(
                "RoomOrders");

            migrationBuilder.DropTable(
                "Desks");

            migrationBuilder.DropTable(
                "Diverses");

            migrationBuilder.DropTable(
                "Rooms");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "CompanyLocations");

            migrationBuilder.DropTable(
                "Companies");
        }
    }
}