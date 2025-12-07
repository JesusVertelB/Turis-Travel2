using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_paquete", Name = "ID_paquete")]
public partial class Transporte
{
    [Key]
    public int ID_transporte { get; set; }

    public int? ID_paquete { get; set; }

    [StringLength(50)]
    public string Tipo_vehiculo { get; set; } = null!;

    public int? Capacidad { get; set; }

    public int? Disponibilidad { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [InverseProperty("ID_transporteNavigation")]
    public virtual ICollection<Asignacion_Conductore> Asignacion_Conductores { get; set; } = new List<Asignacion_Conductore>();

    [ForeignKey("ID_paquete")]
    [InverseProperty("Transportes")]
    public virtual Paquetes_Turistico? ID_paqueteNavigation { get; set; }

    [InverseProperty("ID_transporteNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
