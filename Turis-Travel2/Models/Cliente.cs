using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Cedula { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public string? Estado { get; set; }

    public int IdRol { get; set; }
    public Role IdRolNavigation { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
