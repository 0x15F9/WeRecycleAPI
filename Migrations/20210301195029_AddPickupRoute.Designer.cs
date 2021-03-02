﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210301195029_AddPickupRoute")]
    partial class AddPickupRoute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("API.Models.Bin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int>("Material")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Bins");
                });

            modelBuilder.Entity("API.Models.Pickup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AfterImage")
                        .HasColumnType("text");

                    b.Property<string>("BeforeImage")
                        .HasColumnType("text");

                    b.Property<string>("RouteId")
                        .HasColumnType("text");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Pickups");
                });

            modelBuilder.Entity("API.Models.Route", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.Admin", b =>
                {
                    b.HasBaseType("API.Models.User");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("API.Models.Driver", b =>
                {
                    b.HasBaseType("API.Models.User");

                    b.Property<string>("DrivingLicense")
                        .HasColumnType("text");

                    b.Property<string>("IdCard")
                        .HasColumnType("text");

                    b.Property<string>("ProofOfAddress")
                        .HasColumnType("text");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("API.Models.Pickup", b =>
                {
                    b.HasOne("API.Models.Route", null)
                        .WithMany("Pickups")
                        .HasForeignKey("RouteId");
                });

            modelBuilder.Entity("API.Models.Route", b =>
                {
                    b.HasOne("API.Models.Driver", "Driver")
                        .WithMany("Routes")
                        .HasForeignKey("DriverId");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("API.Models.Admin", b =>
                {
                    b.HasOne("API.Models.User", null)
                        .WithOne()
                        .HasForeignKey("API.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Driver", b =>
                {
                    b.HasOne("API.Models.User", null)
                        .WithOne()
                        .HasForeignKey("API.Models.Driver", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Route", b =>
                {
                    b.Navigation("Pickups");
                });

            modelBuilder.Entity("API.Models.Driver", b =>
                {
                    b.Navigation("Routes");
                });
#pragma warning restore 612, 618
        }
    }
}
