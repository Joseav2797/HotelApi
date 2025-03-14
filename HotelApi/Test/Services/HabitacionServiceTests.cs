using HotelApi.Data;
using HotelApi.Models;
using HotelApi.Services;
using HotelApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace HotelApi.Test.Services
{
    public class HabitacionServiceTests
    {
        private ApplicationDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CrearAsync_DeberiaAgregarHabitacion()
        {
            using var context = GetDbContext(nameof(CrearAsync_DeberiaAgregarHabitacion));
            var service = new HabitacionService(context);

            //  DTO de entrada simulando datos de la nueva habitación
            var habitacionDto = new HabitacionDTO
            {
                Numero = "102",
                Tipo = "Suite",
                Precio = 200,
                Disponible = true
            };

            //  Llamamos al método del servicio
            var result = await service.CrearAsync(habitacionDto);

            //  Verificamos que la habitación se creó correctamente
            Assert.NotNull(result);
            Assert.Equal("102", result.Numero);
            Assert.Equal("Suite", result.Tipo);
            Assert.Equal(200, result.Precio);
            Assert.True(result.Disponible);
        }
    }
}

