﻿// <auto-generated />
using System;
using Alten.CarTracker.Services.StatusReceivedService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace Alten.CarTracker.Services.StatusReceivedService.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alten.CarTracker.Services.StatusReceivedService.Domain.Model.Car", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pkCarId")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = "YS2R4X20005399401"
                        },
                        new
                        {
                            Id = "VLUR4X20009093588"
                        },
                        new
                        {
                            Id = "VLUR4X20009048044"
                        },
                        new
                        {
                            Id = "YS2R4X20005388011"
                        },
                        new
                        {
                            Id = "YS2R4X20005387949"
                        },
                        new
                        {
                            Id = "VLUR4X20009048066"
                        },
                        new
                        {
                            Id = "YS2R4X20005387055"
                        });
                });

            modelBuilder.Entity("Alten.CarTracker.Services.StatusReceivedService.Domain.Model.CarStatusLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pkCarStatusLookupId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("CarStatusesLookup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Stopped"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Moving"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Connected"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Disconnected"
                        });
                });

            modelBuilder.Entity("Alten.CarTracker.Services.StatusReceivedService.Domain.Model.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pkStatusId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarId")
                        .IsRequired();

                    b.Property<Point>("Location")
                        .IsRequired();

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("StatusId");

                    b.ToTable("CarStatuses");
                });

            modelBuilder.Entity("Alten.CarTracker.Services.StatusReceivedService.Domain.Model.Status", b =>
                {
                    b.HasOne("Alten.CarTracker.Services.StatusReceivedService.Domain.Model.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Alten.CarTracker.Services.StatusReceivedService.Domain.Model.CarStatusLookup", "CarStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
