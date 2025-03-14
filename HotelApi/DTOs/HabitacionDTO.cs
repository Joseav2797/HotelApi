using System.ComponentModel.DataAnnotations;

namespace HotelApi.DTOs
{
    public class HabitacionDTO
    {
        [Required(ErrorMessage = "El número de habitación es obligatorio.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El tipo de habitación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de habitación no puede tener más de 50 caracteres.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(1, 10000, ErrorMessage = "El precio debe estar entre 1 y 10,000.")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }
    }
}