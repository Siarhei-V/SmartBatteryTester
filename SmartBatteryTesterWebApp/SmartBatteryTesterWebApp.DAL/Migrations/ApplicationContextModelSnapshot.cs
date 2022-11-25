﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartBatteryTesterWebApp.DAL.EF;

#nullable disable

namespace SmartBatteryTesterWebApp.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartBatteryTesterWebApp.DAL.Entities.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Current")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MeasurementDateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeasurementSetId")
                        .HasColumnType("int");

                    b.Property<decimal>("Voltage")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementSetId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("SmartBatteryTesterWebApp.DAL.Entities.MeasurementSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan?>("DischargeDuration")
                        .HasColumnType("time");

                    b.Property<string>("MeasurementName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeasurementStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ResultCapacity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("MeasurementSets");
                });

            modelBuilder.Entity("SmartBatteryTesterWebApp.DAL.Entities.Measurement", b =>
                {
                    b.HasOne("SmartBatteryTesterWebApp.DAL.Entities.MeasurementSet", "SetOfMeasurements")
                        .WithMany()
                        .HasForeignKey("MeasurementSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SetOfMeasurements");
                });
#pragma warning restore 612, 618
        }
    }
}
