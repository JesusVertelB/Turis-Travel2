using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("Nombre_paquete", Name = "idx_paquete_nombre", IsUnique = true)]
public partial class Paquetes_Turistico
{
    [Key]
    public int ID_paquete { get; set; }

    [StringLength(100)]
    public string Nombre_paquete { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    [Precision(10, 2)]
    public decimal Precio_base { get; set; }

    public DateOnly? Fecha_inicio { get; set; }

    public DateOnly? Fecha_fin { get; set; }

    public int? Capacidad_maxima { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_creacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_actualizacion { get; set; }

    [InverseProperty("ID_paqueteNavigation")]
    public virtual ICollection<Itinerario> Itinerarios { get; set; } = new List<Itinerario>();

    [InverseProperty("ID_paqueteNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    [InverseProperty("ID_paqueteNavigation")]
    public virtual ICollection<Transporte> Transportes { get; set; } = new List<Transporte>();

    [ForeignKey("ID_paquete")]
    [InverseProperty("ID_paquetes")]
    public virtual ICollection<Servicio> ID_servicios { get; set; } = new List<Servicio>();
}
