using HotelApi.DTOs;
using HotelApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelApi.Services
{
    public interface IHabitacionService
    {
        Task<List<Habitacion>> ObtenerTodasAsync();
        Task<Habitacion> ObtenerPorIdAsync(int id);
        Task<Habitacion> CrearAsync(HabitacionDTO habitacionDto);
        Task<bool> ActualizarAsync(int id, HabitacionDTO habitacionDto);
        Task<bool> EliminarAsync(int id);
    }
}
