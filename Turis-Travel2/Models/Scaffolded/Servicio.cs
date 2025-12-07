using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("Nombre_servicio", Name = "Nombre_servicio", IsUnique = true)]
public partial class Servicio
{
    [Key]
    public int ID_servicio { get; set; }

    [StringLength(100)]
    public string Nombre_servicio { get; set; } = null!;

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("ID_servicio")]
    [InverseProperty("ID_servicios")]
    public virtual ICollection<Paquetes_Turistico> ID_paquetes { get; set; } = new List<Paquetes_Turistico>();
}
