using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_paquete", Name = "ID_paquete")]
[Index("Nombre_itinerario", Name = "idx_itinerario_nombre")]
public partial class Itinerario
{
    [Key]
    public int ID_itinerario { get; set; }

    public int? ID_paquete { get; set; }

    [StringLength(100)]
    public string Nombre_itinerario { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    [StringLength(50)]
    public string? Tipo_itinerario { get; set; }

    [StringLength(250)]
    public string? Actividades { get; set; }

    public int? Duracion_dias { get; set; }

    public DateOnly? Fecha_inicio { get; set; }

    public DateOnly? Fecha_fin { get; set; }

    [Precision(10, 2)]
    public decimal? Precio_por_persona { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("ID_paquete")]
    [InverseProperty("Itinerarios")]
    public virtual Paquetes_Turistico? ID_paqueteNavigation { get; set; }

    [InverseProperty("ID_itinerarioNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
