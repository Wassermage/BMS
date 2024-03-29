﻿// <auto-generated />
using System;
using BMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BMS.Migrations
{
    [DbContext(typeof(BmsDbContext))]
    [Migration("20240310235950_Added_Employee_Module")]
    partial class Added_Employee_Module
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BMS.Data.Models.AccessControlGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("AllowedEntryHour")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("AccessControlGroups");
                });

            modelBuilder.Entity("BMS.Data.Models.AccessControlGroupRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessControlGroupId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccessControlGroupId");

                    b.HasIndex("RoomId");

                    b.ToTable("AccessControlGroupRooms");
                });

            modelBuilder.Entity("BMS.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessControlGroupId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("date");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccessControlGroupId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BMS.Data.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BMS.Data.Models.TemperatureReader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("TemperatureReaders");
                });

            modelBuilder.Entity("BMS.Data.Models.TemperatureReadout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.Property<int>("TemperatureReaderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("readoutTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TemperatureReaderId");

                    b.ToTable("TemperatureReadouts");
                });

            modelBuilder.Entity("BMS.Data.Models.AccessControlGroupRoom", b =>
                {
                    b.HasOne("BMS.Data.Models.AccessControlGroup", "AccessControlGroup")
                        .WithMany("AccessControlGroupRooms")
                        .HasForeignKey("AccessControlGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BMS.Data.Models.Room", "Room")
                        .WithMany("AccessControlGroupRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessControlGroup");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BMS.Data.Models.Employee", b =>
                {
                    b.HasOne("BMS.Data.Models.AccessControlGroup", "AccessControlGroup")
                        .WithMany("Employees")
                        .HasForeignKey("AccessControlGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessControlGroup");
                });

            modelBuilder.Entity("BMS.Data.Models.TemperatureReader", b =>
                {
                    b.HasOne("BMS.Data.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BMS.Data.Models.TemperatureReadout", b =>
                {
                    b.HasOne("BMS.Data.Models.TemperatureReader", "TemperatureReader")
                        .WithMany()
                        .HasForeignKey("TemperatureReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TemperatureReader");
                });

            modelBuilder.Entity("BMS.Data.Models.AccessControlGroup", b =>
                {
                    b.Navigation("AccessControlGroupRooms");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("BMS.Data.Models.Room", b =>
                {
                    b.Navigation("AccessControlGroupRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
