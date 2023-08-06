﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi1.Data;

#nullable disable

namespace WebApi1.Migrations
{
    [DbContext(typeof(AplicationDboContext))]
    [Migration("20230803073808_AlimentarTablaVilla")]
    partial class AlimentarTablaVilla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi1.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MetrosCuadrados")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "muchas",
                            Detalle = "casita chida",
                            FechaActualizacion = new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3613),
                            FechaCreacion = new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3598),
                            ImgUrl = "",
                            MetrosCuadrados = 30.0,
                            Nombre = "Villa del real tecamac",
                            Ocupantes = 6,
                            Tarifa = 1234.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "2 pisos",
                            Detalle = "privada exclusiva",
                            FechaActualizacion = new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3618),
                            FechaCreacion = new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3617),
                            ImgUrl = "",
                            MetrosCuadrados = 40.0,
                            Nombre = "heroes de tecamac",
                            Ocupantes = 3,
                            Tarifa = 7890.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}