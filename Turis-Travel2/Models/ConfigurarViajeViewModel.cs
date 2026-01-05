using System;
using System.ComponentModel.DataAnnotations;

namespace Turis_Travel2.Models.ViewModels
{
    public class ConfigurarViajeViewModel
    {
        
        [Required]
        public int IdDestino { get; set; }

        public string NombreDestino { get; set; } = string.Empty;

        public decimal PrecioBase { get; set; }


        [Required(ErrorMessage = "La fecha de salida es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Debe haber al menos 1 adulto")]
        public int CantidadAdultos { get; set; }

        [Range(0, 10)]
        public int CantidadNinos { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }

        public int TotalPersonas =>
            CantidadAdultos + CantidadNinos;

        public decimal PrecioTotal =>
            PrecioBase * TotalPersonas;
    }
}
