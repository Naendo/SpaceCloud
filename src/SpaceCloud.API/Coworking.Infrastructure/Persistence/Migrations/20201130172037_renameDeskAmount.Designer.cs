﻿// <auto-generated />
using System;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coworking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(WorkingContext))]
    [Migration("20201130172037_renameDeskAmount")]
    partial class renameDeskAmount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Coworking.Domain.Entities.BillingAddress", b =>
                {
                    b.Property<int>("BillingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Door")
                        .HasColumnType("text");

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BillingId");

                    b.ToTable("BillingAddresses");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Usage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SettingsId")
                        .HasColumnType("integer");

                    b.Property<string>("SubDomain")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CompanyId");

                    b.HasIndex("SettingsId")
                        .IsUnique();

                    b.HasIndex("SubDomain")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.CompanyLocation", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Door")
                        .HasColumnType("text");

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LocationId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyLocations");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.CompanySettings", b =>
                {
                    b.Property<int>("SettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CurrencyAmountPerSubscription")
                        .HasColumnType("integer");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LogoUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("StartingInvoiceNr")
                        .HasColumnType("integer");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WebsiteUri")
                        .HasColumnType("text");

                    b.HasKey("SettingsId");

                    b.ToTable("CompanySettings");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Desk", b =>
                {
                    b.Property<int>("DeskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IntervalType")
                        .HasColumnType("integer");

                    b.Property<int>("ProductAmount")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("DeskId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Desks");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Identity", b =>
                {
                    b.Property<int>("IdentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastLogged")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PasswordResetTokenExpires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("StayLogged")
                        .HasColumnType("boolean");

                    b.HasKey("IdentityId");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InvoiceNr")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("PdfUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("InvoiceId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeclined")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedByUserId")
                        .HasColumnType("integer");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("ProductId");

                    b.HasIndex("LastModifiedByUserId");

                    b.HasIndex("LocationId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RecentEvent", b =>
                {
                    b.Property<int>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ActionId");

                    b.HasIndex("UserId");

                    b.ToTable("RecentEvents");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("RoomId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RoomTags", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.HasKey("TagId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomTags");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.SpaceCoin", b =>
                {
                    b.Property<int>("CoinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("CoinGuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CoinId");

                    b.HasIndex("UserId");

                    b.ToTable("SpaceCoins");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("TicketId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("BillingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IdentityId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("BillingId")
                        .IsUnique();

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.HasIndex("LocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Coworking.Domain.Scheduler", b =>
                {
                    b.Property<int>("SchedulerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ActivatorToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastLogged")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReferenceToken")
                        .HasColumnType("text");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("SchedulerId");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Schedulers");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Cart", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.Entities.Product", "Product")
                        .WithMany("CartEntries")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Company", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.CompanySettings", "Settings")
                        .WithOne("Company")
                        .HasForeignKey("Coworking.Domain.Entities.Company", "SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.CompanyLocation", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Company", "Company")
                        .WithMany("Locations")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Desk", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Product", "Product")
                        .WithOne("Desk")
                        .HasForeignKey("Coworking.Domain.Entities.Desk", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Order", "Order")
                        .WithOne("Invoice")
                        .HasForeignKey("Coworking.Domain.Entities.Invoice", "OrderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Order", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Product", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.User", "LastModifiedBy")
                        .WithMany("Products")
                        .HasForeignKey("LastModifiedByUserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.Entities.CompanyLocation", "Location")
                        .WithMany("Products")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RecentEvent", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Room", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Product", "Product")
                        .WithOne("Room")
                        .HasForeignKey("Coworking.Domain.Entities.Room", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RoomTags", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Room", "Room")
                        .WithMany("Tags")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.SpaceCoin", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.User", "User")
                        .WithMany("SpaceCoins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.User", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.BillingAddress", "BillingAddress")
                        .WithOne("User")
                        .HasForeignKey("Coworking.Domain.Entities.User", "BillingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Coworking.Domain.Entities.Identity", "Identity")
                        .WithOne("User")
                        .HasForeignKey("Coworking.Domain.Entities.User", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.Entities.CompanyLocation", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Scheduler", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Room", "Room")
                        .WithOne("Scheduler")
                        .HasForeignKey("Coworking.Domain.Scheduler", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.Entities.User", "User")
                        .WithMany("Schedulers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
