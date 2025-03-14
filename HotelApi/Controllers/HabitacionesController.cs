using HotelApi.Services;
using HotelApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApi.DTOs;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionesController : ControllerBase
    {
        private readonly IHabitacionService _habitacionService;

        public HabitacionesController(IHabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }

        // GET: api/habitaciones
        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult<IEnumerable<Habitacion>>> GetHabitaciones()
        {
            try
            {
                var habitaciones = await _habitacionService.ObtenerTodasAsync();
                Log.Information("Se obtuvo la lista de habitaciones.");
                return Ok(habitaciones);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al obtener habitaciones: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // GET: api/habitaciones/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Habitacion>> GetHabitacion(int id)
        {
            try
            {
                var habitacion = await _habitacionService.ObtenerPorIdAsync(id);
                if (habitacion == null)
                {
                    Log.Warning($"Habitación con ID {id} no encontrada.");
                    return NotFound(new { mensaje = "Habitación no encontrada" });
                }

                Log.Information($"Se obtuvo la habitación con ID {id}.");
                return Ok(habitacion);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al obtener la habitación con ID {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // POST: api/habitaciones
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<Habitacion>> CreateHabitacion(HabitacionDTO habitacionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nuevaHabitacion = await _habitacionService.CrearAsync(habitacionDto);
                Log.Information($"Se creó una nueva habitación con ID {nuevaHabitacion.Id}.");
                return CreatedAtAction(nameof(GetHabitacion), new { id = nuevaHabitacion.Id }, nuevaHabitacion);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al crear la habitación: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // PUT: api/habitaciones/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateHabitacion(int id, HabitacionDTO habitacionDto)
        {
            try
            {
                var resultado = await _habitacionService.ActualizarAsync(id, habitacionDto);
                if (!resultado)
                {
                    Log.Warning($"Intento de actualizar habitación no encontrada con ID {id}.");
                    return NotFound(new { mensaje = "Habitación no encontrada" });
                }

                Log.Information($"Se actualizó la habitación con ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"Error al actualizar la habitación con ID {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // DELETE: api/habitaciones/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            try
            {
                var resultado = await _habitacionService.EliminarAsync(id);
                if (!resultado)
                {
                    Log.Warning($"Intento de eliminar habitación no encontrada con ID {id}.");
                    return NotFound(new { mensaje = "Habitación no encontrada" });
                }

                Log.Information($"Se eliminó la habitación con ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"Error al eliminar la habitación con ID {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}

