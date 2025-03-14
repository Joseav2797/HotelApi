using HotelApi.Controllers;
using HotelApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using HotelApi.Models;
using System.Threading.Tasks;
using HotelApi.DTOs;

namespace HotelApi.Test.Controllers
{
    public class ReservasControllerTests
    {
        [Fact]
        public async Task CreateReserva_DeberiaCrearReserva()
        {
            //  Mock de IReservaService en lugar de ReservaService
            var mockService = new Mock<IReservaService>();

            var reservaDto = new ReservaDTO
            {
                HabitacionId = 1,
                ClienteNombre = "Juan Pérez",
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(3)
            };

            var reserva = new Reserva
            {
                Id = 1,
                ClienteNombre = "Juan Pérez",
                HabitacionId = 1,
                FechaEntrada = reservaDto.FechaEntrada,
                FechaSalida = reservaDto.FechaSalida
            };

            //  Simulamos que el servicio devolverá la reserva creada
            mockService.Setup(s => s.CrearAsync(It.IsAny<Reserva>())).ReturnsAsync(reserva);


            // Inyectamos el servicio mockeado al controlador
            var controller = new ReservasController(mockService.Object);

            //  Llamamos al método CreateReserva (antes llamado PostReserva)
            var result = await controller.CreateReserva(reservaDto);

            //  Verificamos que devuelve CreatedAtActionResult
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdReserva = Assert.IsType<Reserva>(createdAtResult.Value);
            Assert.Equal("Juan Pérez", createdReserva.ClienteNombre);
        }
    }
}

