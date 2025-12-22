using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class PaquetesTuristico
{
    public int IdPaquete { get; set; }

    public string NombrePaquete { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioBase { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? CapacidadMaxima { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? ImagenUrl { get; set; }

    public virtual ICollection<Itinerario> Itinerarios { get; set; } = new List<Itinerario>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Transporte> Transportes { get; set; } = new List<Transporte>();

    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();



}
