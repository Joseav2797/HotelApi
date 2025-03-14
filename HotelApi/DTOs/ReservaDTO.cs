using System;
using System.ComponentModel.DataAnnotations;

namespace HotelApi.DTOs
{
    public class ReservaDTO
    {
        [Required(ErrorMessage = "El ID de la habitación es obligatorio.")]
        public int HabitacionId { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del cliente no puede tener más de 100 caracteres.")]
        public string ClienteNombre { get; set; }

        [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
        public DateTime FechaEntrada { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        [DateGreaterThan(nameof(FechaEntrada), ErrorMessage = "La fecha de salida debe ser posterior a la fecha de entrada.")]
        public DateTime FechaSalida { get; set; }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonProperty == null)
            {
                return new ValidationResult($"Propiedad {_comparisonProperty} no encontrada.");
            }

            var comparisonValue = (DateTime)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
