using HotelApi.Controllers;
using HotelApi.DTOs;
using HotelApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelApi.Services
{
    public interface IReservaService
    {
        Task<List<Reserva>> ObtenerReservasAsync();
        Task<Reserva> ObtenerReservaPorIdAsync(int id); 
        Task<Reserva> CrearAsync(Reserva reserva); 
        Task<bool> EliminarReservaAsync(int id);
      
    }
}