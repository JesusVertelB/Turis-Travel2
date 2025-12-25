using System;
using System.ComponentModel.DataAnnotations;

namespace Turis_Travel2.Models.ViewModels
{
    public class ConfigurarViajeViewModel
    {
        [Required]
        public int IdDestino { get; set; }

        [Required]
        public string NombreDestino { get; set; } = string.Empty;

        [Required]
        public DateTime FechaSalida { get; set; }

        [Required]
        [Range(1, 20)]
        public int CantidadPersonas { get; set; }

        public string? Observaciones { get; set; }
    }
}
