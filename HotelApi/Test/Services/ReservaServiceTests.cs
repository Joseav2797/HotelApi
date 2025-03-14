using HotelApi.Data;
using HotelApi.Models;
using HotelApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HotelApi.Test.Services
{
    public class ReservaServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CrearAsync_DeberiaCrearReservaSiHabitacionDisponible()
        {
            var context = GetDbContext();
            var habitacion = new Habitacion { Id = 1, Numero = "101", Tipo = "Doble", Precio = 100, Disponible = true };
            context.Habitaciones.Add(habitacion);
            await context.SaveChangesAsync();

            var service = new ReservaService(context);
            var reserva = new Reserva
            {
                HabitacionId = 1,
                ClienteNombre = "Juan Pérez",
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2)
            };

            var result = await service.CrearAsync(reserva);

            Assert.NotNull(result);
            Assert.False(habitacion.Disponible);
        }

        [Fact]
        public async Task CrearAsync_DeberiaLanzarExcepcionSiHabitacionNoDisponible()
        {
            var context = GetDbContext();
            var habitacion = new Habitacion { Id = 1, Numero = "101", Tipo = "Doble", Precio = 100, Disponible = false };
            context.Habitaciones.Add(habitacion);
            await context.SaveChangesAsync();

            var service = new ReservaService(context);
            var reserva = new Reserva
            {
                HabitacionId = 1,
                ClienteNombre = "Juan Pérez",
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2)
            };

            await Assert.ThrowsAsync<Exception>(async () => await service.CrearAsync(reserva));
        }
    }
}
