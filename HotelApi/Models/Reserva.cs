using System.ComponentModel.DataAnnotations;
using System;

namespace HotelApi.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required]
        public int HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del cliente no puede exceder los 100 caracteres")]
        public string ClienteNombre { get; set; }

        [Required]
        public DateTime FechaEntrada { get; set; }

        [Required]
        [FechaMayorQue("FechaEntrada", ErrorMessage = "La fecha de salida debe ser posterior a la fecha de entrada")]
        public DateTime FechaSalida { get; set; }
    }


    public class FechaMayorQueAttribute : ValidationAttribute
    {
        private readonly string _propiedadComparar;

        public FechaMayorQueAttribute(string propiedadComparar)
        {
            _propiedadComparar = propiedadComparar;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propiedad = validationContext.ObjectType.GetProperty(_propiedadComparar);

            if (propiedad == null)
            {
                return new ValidationResult($"No se encuentra la propiedad {_propiedadComparar}");
            }

            var fechaComparar = (DateTime)propiedad.GetValue(validationContext.ObjectInstance);
            var fechaActual = (DateTime)value;

            if (fechaActual <= fechaComparar)
            {
                return new ValidationResult(ErrorMessage ?? "La fecha de salida debe ser posterior a la fecha de entrada");
            }

            return ValidationResult.Success;
        }
    }
}
