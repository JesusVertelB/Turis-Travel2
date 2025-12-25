using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turis_Travel2.Models;

public partial class PaquetesTuristicos
{

    [Column("ID_paquete")]
    public int IdPaquete { get; set; }

    [Column("Id_Destino")]
    public string? IdDestino { get; set; }

    [Column("Nombre_paquete")]
    public string NombrePaquete { get; set; } = null!;

    public string? Descripcion { get; set; }

    [Column("Precio_base")]
    public decimal PrecioBase { get; set; }

    [Column("Fecha_inicio")]
    public DateOnly? FechaInicio { get; set; }

    [Column("Fecha_fin")]
    public DateOnly? FechaFin { get; set; }

    [Column("Capacidad_maxima")]
    public int? CapacidadMaxima { get; set; }

    public string? Estado { get; set; }

    [Column("Fecha_creacion")]
    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? ImagenUrl { get; set; }

    public virtual ICollection<Itinerario> Itinerarios { get; set; } = new List<Itinerario>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Transporte> Transportes { get; set; } = new List<Transporte>();

    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();

    public virtual ICollection<Destino> IdDestinoNavigation { get; set; } = new List<Destino>();



}
