using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

public partial class Conductore
{
    [Key]
    public int ID_conductor { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string? Licencia { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [InverseProperty("ID_conductorNavigation")]
    public virtual ICollection<Asignacion_Conductore> Asignacion_Conductores { get; set; } = new List<Asignacion_Conductore>();
}
