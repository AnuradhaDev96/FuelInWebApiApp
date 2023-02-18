﻿// <auto-generated />
using FuelInApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FuelInApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230218055744_AddFuelStationToDb3")]
    partial class AddFuelStationToDb3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FuelInApi.Models.FuelInVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("FuelInVehicles");
                });

            modelBuilder.Entity("FuelInApi.Models.FuelInVehicleOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SystemUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemUserId");

                    b.ToTable("FuelInVehicleOwners");
                });

            modelBuilder.Entity("FuelInApi.Models.FuelStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalAuthority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerUserId")
                        .HasColumnType("int");

                    b.Property<double>("PopulationDensity")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("FuelStations");
                });

            modelBuilder.Entity("FuelInApi.Models.SystemUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("FuelInApi.Models.VehicleRegistry", b =>
                {
                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlateNumber");

                    b.ToTable("VehicleRegistry");
                });

            modelBuilder.Entity("FuelInApi.Models.FuelInVehicle", b =>
                {
                    b.HasOne("FuelInApi.Models.FuelInVehicleOwner", "Owner")
                        .WithOne("Vehicle")
                        .HasForeignKey("FuelInApi.Models.FuelInVehicle", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("FuelInApi.Models.FuelInVehicleOwner", b =>
                {
                    b.HasOne("FuelInApi.Models.SystemUser", "SystemUser")
                        .WithMany()
                        .HasForeignKey("SystemUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemUser");
                });

            modelBuilder.Entity("FuelInApi.Models.FuelStation", b =>
                {
                    b.HasOne("FuelInApi.Models.SystemUser", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FuelInApi.Models.FuelInVehicleOwner", b =>
                {
                    b.Navigation("Vehicle")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}