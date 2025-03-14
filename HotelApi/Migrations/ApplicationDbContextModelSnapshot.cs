﻿// <auto-generated />
using System;
using HotelApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("HotelApi.Models.Habitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Disponible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Habitaciones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Disponible = true,
                            Numero = "101",
                            Precio = 1000m,
                            Tipo = "Individual"
                        },
                        new
                        {
                            Id = 2,
                            Disponible = true,
                            Numero = "102",
                            Precio = 1500m,
                            Tipo = "Doble"
                        });
                });

            modelBuilder.Entity("HotelApi.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClienteNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaEntrada")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaSalida")
                        .HasColumnType("TEXT");

                    b.Property<int>("HabitacionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HabitacionId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("HotelApi.Models.Reserva", b =>
                {
                    b.HasOne("HotelApi.Models.Habitacion", "Habitacion")
                        .WithMany("Reservas")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habitacion");
                });

            modelBuilder.Entity("HotelApi.Models.Habitacion", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
