using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("Nombre_rol", Name = "Nombre_rol", IsUnique = true)]
public partial class Role
{
    [Key]
    public int ID_rol { get; set; }

    [StringLength(80)]
    public string Nombre_rol { get; set; } = null!;

    public int? Estado_rol { get; set; }

    [InverseProperty("ID_rolNavigation")]
    [Required(ErrorMessage = "El nombre del rol es obligatorio")]
    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();

    [InverseProperty("ID_rolNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
