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
    [Migration("20200908165843_OrderRefactoring")]
    partial class OrderRefactoring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Coworking.Domain.BillingAdress", b =>
                {
                    b.Property<int>("BId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Door")
                        .HasColumnType("text");

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("ZIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("BillingAdress");
                });

            modelBuilder.Entity("Coworking.Domain.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Coworking.Domain.CompanyLocation", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Door")
                        .HasColumnType("text");

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<string>("ZIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LocationId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyLocations");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Desk", b =>
                {
                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AmountOfDesks")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int>("ModifiedByUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PricePerMonth")
                        .HasColumnType("numeric");

                    b.HasKey("Pid");

                    b.HasIndex("LocationId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("Desks");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.DeskOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Pid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("OrderId");

                    b.HasIndex("Pid");

                    b.ToTable("DeskOrders");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Diverse", b =>
                {
                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AmountOfDesks")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int>("ModifiedByUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PricePerMonth")
                        .HasColumnType("numeric");

                    b.HasKey("Pid");

                    b.HasIndex("LocationId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("Diverses");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.DiverseOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Pid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("OrderId");

                    b.HasIndex("Pid");

                    b.ToTable("DiverseOrders");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DeskOrderId")
                        .HasColumnType("integer");

                    b.Property<int?>("DiverseOrderId")
                        .HasColumnType("integer");

                    b.Property<int?>("InvoiceNr")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDenied")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("boolean");

                    b.Property<string>("PdfUri")
                        .HasColumnType("text");

                    b.Property<int?>("RoomOrderId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("InvoiceId");

                    b.HasIndex("DeskOrderId")
                        .IsUnique();

                    b.HasIndex("DiverseOrderId")
                        .IsUnique();

                    b.HasIndex("RoomOrderId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.MeetingRoom", b =>
                {
                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int>("ModifiedByUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PricePerHour")
                        .HasColumnType("numeric");

                    b.HasKey("Pid");

                    b.HasIndex("LocationId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.OperationTime", b =>
                {
                    b.Property<int>("OtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Closing")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Opening")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Pid")
                        .HasColumnType("integer");

                    b.HasKey("OtId");

                    b.HasIndex("Pid");

                    b.ToTable("OperationTime");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RecentAction", b =>
                {
                    b.Property<int>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ActionId");

                    b.ToTable("RecentActions");
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RoomOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Pid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("Pid");

                    b.HasIndex("UserId");

                    b.ToTable("RoomOrders");
                });

            modelBuilder.Entity("Coworking.Domain.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
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

            modelBuilder.Entity("Coworking.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<int>("CompanyLocationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<bool>("IsPermittedUser")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastLogged")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompanyLocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Coworking.Domain.UserIdentity", b =>
                {
                    b.Property<int>("AuthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ResetToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("StayLogged")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("AuthId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserIdentity");
                });

            modelBuilder.Entity("Coworking.Domain.BillingAdress", b =>
                {
                    b.HasOne("Coworking.Domain.User", "User")
                        .WithOne("BillingAdress")
                        .HasForeignKey("Coworking.Domain.BillingAdress", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.CompanyLocation", b =>
                {
                    b.HasOne("Coworking.Domain.Company", "Company")
                        .WithMany("Locations")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Desk", b =>
                {
                    b.HasOne("Coworking.Domain.CompanyLocation", "Location")
                        .WithMany("Desks")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.User", "LastModifiedUser")
                        .WithMany("CreatedDeskProducts")
                        .HasForeignKey("ModifiedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.DeskOrder", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Desk", "Desk")
                        .WithMany("Orders")
                        .HasForeignKey("Pid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Diverse", b =>
                {
                    b.HasOne("Coworking.Domain.CompanyLocation", "Location")
                        .WithMany("Diverses")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.User", "LastModifiedUser")
                        .WithMany("CreatedDiverseProducts")
                        .HasForeignKey("ModifiedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.DiverseOrder", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.Diverse", "Diverse")
                        .WithMany("Orders")
                        .HasForeignKey("Pid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.DeskOrder", "DeskOrder")
                        .WithOne("Invoice")
                        .HasForeignKey("Coworking.Domain.Entities.Invoice", "DeskOrderId");

                    b.HasOne("Coworking.Domain.Entities.DiverseOrder", "DiverseOrder")
                        .WithOne("Invoice")
                        .HasForeignKey("Coworking.Domain.Entities.Invoice", "DiverseOrderId");

                    b.HasOne("Coworking.Domain.Entities.RoomOrder", "RoomOrder")
                        .WithOne("Invoice")
                        .HasForeignKey("Coworking.Domain.Entities.Invoice", "RoomOrderId");

                    b.HasOne("Coworking.Domain.User", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.MeetingRoom", b =>
                {
                    b.HasOne("Coworking.Domain.CompanyLocation", "Location")
                        .WithMany("Rooms")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.User", "LastModifiedUser")
                        .WithMany("CreatedRoomProducts")
                        .HasForeignKey("ModifiedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.OperationTime", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.MeetingRoom", "Room")
                        .WithMany("OperationTimes")
                        .HasForeignKey("Pid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.Entities.RoomOrder", b =>
                {
                    b.HasOne("Coworking.Domain.Entities.MeetingRoom", "Room")
                        .WithMany("Orders")
                        .HasForeignKey("Pid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coworking.Domain.User", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Coworking.Domain.Ticket", b =>
                {
                    b.HasOne("Coworking.Domain.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.User", b =>
                {
                    b.HasOne("Coworking.Domain.Company", "AdministratingCompany")
                        .WithMany("Administrator")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Coworking.Domain.CompanyLocation", "CompanyLocation")
                        .WithMany("Users")
                        .HasForeignKey("CompanyLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coworking.Domain.UserIdentity", b =>
                {
                    b.HasOne("Coworking.Domain.User", "User")
                        .WithOne("Identity")
                        .HasForeignKey("Coworking.Domain.UserIdentity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
