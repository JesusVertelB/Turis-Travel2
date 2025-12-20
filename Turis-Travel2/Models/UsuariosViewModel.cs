using System;
using System.Collections.Generic;
using Turis_Travel2.Models;

namespace Turis_Travel2.Models
{
    public class UsuariosViewModel
    {
        public List<Reserva> Reserva { get; set; } = new();
        public List<Usuario> Usuario { get; set; } = new();

        // 🔥 Campos adicionales SOLO para la vista
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Aeropuerto { get; set; }
        public string? TipoViaje { get; set; }
    }
}


