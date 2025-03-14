using HotelApi.Controllers;
using HotelApi.Models;
using HotelApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelApi.Test.Controllers
{
    public class HabitacionesControllerTests
    {
        [Fact]
        public async Task GetHabitaciones_DeberiaRetornarListaDeHabitaciones()
        {
            var mockService = new Mock<HabitacionService>(null);
            mockService.Setup(s => s.ObtenerTodasAsync()).ReturnsAsync(new List<Habitacion>
        {
            new Habitacion { Id = 1, Numero = "101", Tipo = "Doble", Precio = 100, Disponible = true }
        });

            var controller = new HabitacionesController(mockService.Object);
            var result = await controller.GetHabitaciones();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var habitaciones = Assert.IsType<List<Habitacion>>(okResult.Value);
            Assert.Single(habitaciones);
        }
    }
}
