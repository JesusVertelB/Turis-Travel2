using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdCliente { get; set; }

    public int? IdPaquete { get; set; }

    public int? IdItinerario { get; set; }

    public int? IdTransporte { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public string? Estado { get; set; }

    public int? NumeroPasajeros { get; set; }

    public decimal? PrecioTotal { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Itinerario? IdItinerarioNavigation { get; set; }

    public virtual PaquetesTuristicos? IdPaqueteNavigation { get; set; }

    public virtual Transporte? IdTransporteNavigation { get; set; }

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Retroalimentacion> Retroalimentacions { get; set; } = new List<Retroalimentacion>();
}
