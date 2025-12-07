using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("Nombre_modulo", Name = "Nombre_modulo", IsUnique = true)]
public partial class Modulo
{
    [Key]
    public int ID_modulo { get; set; }

    [StringLength(80)]
    public string Nombre_modulo { get; set; } = null!;

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [InverseProperty("ID_moduloNavigation")]
    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
