using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using System.Collections.Generic;

namespace HotelApi.Models
{
    public class Habitacion
    {
        public int Id { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; } = true;

  
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
