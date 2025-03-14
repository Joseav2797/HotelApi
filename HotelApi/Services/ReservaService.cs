using HotelApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelApi.Models;

namespace HotelApi.Services
{
    public class ReservaService : IReservaService
    {
        private readonly ApplicationDbContext _context;

        public ReservaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> ObtenerReservasAsync()
        {
            return await _context.Reservas
                .Include(r => r.Habitacion)
                .ToListAsync();
        }

        public async Task<Reserva> ObtenerReservaPorIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Habitacion)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reserva> CrearAsync(Reserva reserva)
        {
            var habitacion = await _context.Habitaciones.FindAsync(reserva.HabitacionId);
            if (habitacion == null || !habitacion.Disponible)
            {
                throw new Exception("La habitación no está disponible o no existe.");
            }

            reserva.Habitacion = habitacion;
            _context.Reservas.Add(reserva);
            habitacion.Disponible = false;
            await _context.SaveChangesAsync();
            return reserva;
        }

        public async Task<bool> EliminarReservaAsync(int id)
        {
            var reserva = await _context.Reservas.Include(r => r.Habitacion).FirstOrDefaultAsync(r => r.Id == id);
            if (reserva == null)
                return false;

            reserva.Habitacion.Disponible = true;
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

