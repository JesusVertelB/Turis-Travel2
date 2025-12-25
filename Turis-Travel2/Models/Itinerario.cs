using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Itinerario
{
    public int IdItinerario { get; set; }

    public int? IdPaquete { get; set; }

    public string NombreItinerario { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? TipoItinerario { get; set; }

    public string? Actividades { get; set; }

    public int? DuracionDias { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? PrecioPorPersona { get; set; }

    public string? Estado { get; set; }

    public virtual PaquetesTuristicos? IdPaqueteNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
