using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Transporte
{
    public int IdTransporte { get; set; }

    public int? IdPaquete { get; set; }

    public string TipoVehiculo { get; set; } = null!;

    public int? Capacidad { get; set; }

    public int? Disponibilidad { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<AsignacionConductore> AsignacionConductores { get; set; } = new List<AsignacionConductore>();

    public virtual PaquetesTuristico? IdPaqueteNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
