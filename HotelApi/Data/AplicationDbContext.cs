using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using HotelApi.Models;

namespace HotelApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Habitacion)
                .WithMany(h => h.Reservas)
                .HasForeignKey(r => r.HabitacionId);

            
            modelBuilder.Entity<Habitacion>().HasData(
                new Habitacion { Id = 1, Numero = "101", Tipo = "Individual", Precio = 1000, Disponible = true },
                new Habitacion { Id = 2, Numero = "102", Tipo = "Doble", Precio = 1500, Disponible = true }
            );
        }
    }

}
