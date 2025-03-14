using HotelApi.Data;
using HotelApi.DTOs;
using HotelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelApi.Services
{
    public class HabitacionService : IHabitacionService
    {
        private readonly ApplicationDbContext _context;

        public HabitacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Habitacion>> ObtenerTodasAsync()
        {
            return await _context.Habitaciones.ToListAsync();
        }

        public async Task<Habitacion> ObtenerPorIdAsync(int id)
        {
            return await _context.Habitaciones.FindAsync(id);
        }

        public async Task<Habitacion> CrearAsync(HabitacionDTO habitacionDto)
        {
            var habitacion = new Habitacion
            {
                Numero = habitacionDto.Numero,
                Tipo = habitacionDto.Tipo,
                Precio = habitacionDto.Precio,
                Disponible = habitacionDto.Disponible
            };

            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();
            return habitacion;
        }

        public async Task<bool> ActualizarAsync(int id, HabitacionDTO habitacionDto)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null) return false;

            habitacion.Numero = habitacionDto.Numero;
            habitacion.Tipo = habitacionDto.Tipo;
            habitacion.Precio = habitacionDto.Precio;
            habitacion.Disponible = habitacionDto.Disponible;

            _context.Entry(habitacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null) return false;

            _context.Habitaciones.Remove(habitacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
