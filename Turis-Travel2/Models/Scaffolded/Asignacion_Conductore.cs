using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_conductor", Name = "ID_conductor")]
[Index("ID_transporte", Name = "ID_transporte")]
public partial class Asignacion_Conductore
{
    [Key]
    public int ID_asignacion { get; set; }

    public int? ID_conductor { get; set; }

    public int? ID_transporte { get; set; }

    public DateOnly? Fecha_asignacion { get; set; }

    [ForeignKey("ID_conductor")]
    [InverseProperty("Asignacion_Conductores")]
    public virtual Conductore? ID_conductorNavigation { get; set; }

    [ForeignKey("ID_transporte")]
    [InverseProperty("Asignacion_Conductores")]
    public virtual Transporte? ID_transporteNavigation { get; set; }
}
