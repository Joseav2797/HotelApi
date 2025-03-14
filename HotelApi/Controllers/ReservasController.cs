using HotelApi.Services;
using HotelApi.Models;
using HotelApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelApi.Controllers
{
   
    [Route("api/reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        /// <summary>
        /// Obtiene todas las reservas. Solo accesible por administradores.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            var reservas = await _reservaService.ObtenerReservasAsync();

            if (reservas == null || reservas.Count == 0)
            {
                Log.Warning("No se encontraron reservas.");
                return NotFound(new { mensaje = "No hay reservas disponibles." });
            }

            Log.Information("Se obtuvo la lista de reservas.");
            return Ok(reservas);
        }

        /// <summary>
        /// Obtiene una reserva por ID. Accesible por Admin y Usuario.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _reservaService.ObtenerReservaPorIdAsync(id);
            if (reserva == null)
            {
                Log.Warning($"Reserva con ID {id} no encontrada.");
                return NotFound(new { mensaje = "Reserva no encontrada" });
            }

            Log.Information($"Se obtuvo la reserva con ID {id}.");
            return Ok(reserva);
        }

        /// <summary>
        /// Crea una nueva reserva. Accesible por Usuarios.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public async Task<ActionResult<Reserva>> CreateReserva([FromBody] ReservaDTO reservaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convertimos el DTO a la entidad Reserva
            var reserva = new Reserva
            {
                HabitacionId = reservaDto.HabitacionId,
                ClienteNombre = reservaDto.ClienteNombre,
                FechaEntrada = reservaDto.FechaEntrada,
                FechaSalida = reservaDto.FechaSalida
            };

            var nuevaReserva = await _reservaService.CrearAsync(reserva);
            if (nuevaReserva == null)
            {
                Log.Error("Error al intentar crear la reserva.");
                return BadRequest(new { mensaje = "No se pudo crear la reserva." });
            }

            Log.Information($"Se creó una nueva reserva con ID {nuevaReserva.Id}.");
            return CreatedAtAction(nameof(GetReserva), new { id = nuevaReserva.Id }, nuevaReserva);
        }

        /// <summary>
        /// Actualiza una reserva existente. Accesible por Admin y Usuario.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> UpdateReserva(int id, [FromBody] ReservaDTO reservaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservaExistente = await _reservaService.ObtenerReservaPorIdAsync(id);
            if (reservaExistente == null)
            {
                Log.Warning($"Intento de actualizar reserva no encontrada con ID {id}.");
                return NotFound(new { mensaje = "Reserva no encontrada o no se pudo actualizar." });
            }

          
            reservaExistente.HabitacionId = reservaDto.HabitacionId;
            reservaExistente.ClienteNombre = reservaDto.ClienteNombre;
            reservaExistente.FechaEntrada = reservaDto.FechaEntrada;
            reservaExistente.FechaSalida = reservaDto.FechaSalida;

            var resultado = await _reservaService.CrearAsync(reservaExistente); 

            if (resultado == null)
            {
                Log.Error("Error al actualizar la reserva.");
                return BadRequest(new { mensaje = "No se pudo actualizar la reserva." });
            }

            Log.Information($"Se actualizó la reserva con ID {id}.");
            return NoContent();
        }

        /// <summary>
        /// Elimina una reserva. Accesible por Admin y Usuario.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var resultado = await _reservaService.EliminarReservaAsync(id);
            if (!resultado)
            {
                Log.Warning($"Intento de eliminar reserva no encontrada con ID {id}.");
                return NotFound(new { mensaje = "Reserva no encontrada o ya eliminada." });
            }

            Log.Information($"Se eliminó la reserva con ID {id}.");
            return NoContent();
        }
    }
}
