using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int? Estado { get; set; }

    public string? Observacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<RecuperacionContrasena> RecuperacionContrasenas { get; set; } = new List<RecuperacionContrasena>();
}
