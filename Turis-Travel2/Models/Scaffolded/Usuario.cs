using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("Correo", Name = "Correo", IsUnique = true)]
[Index("ID_rol", Name = "ID_rol")]
public partial class Usuario
{
    [Key]
    public int ID_usuario { get; set; }

    public int? ID_rol { get; set; }

    [StringLength(50)]
    public string Nombre_usuario { get; set; } = null!;

    [StringLength(100)]
    public string Correo { get; set; } = null!;

    [StringLength(250)]
    public string Contrasena { get; set; } = null!;

    public int? Estado { get; set; }

    [StringLength(100)]
    public string? Observacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_creacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_actualizacion { get; set; }

    [ForeignKey("ID_rol")]
    [InverseProperty("Usuarios")]
    public virtual Role? ID_rolNavigation { get; set; }

    [InverseProperty("ID_usuarioNavigation")]
    public virtual ICollection<Recuperacion_Contrasena> Recuperacion_Contrasenas { get; set; } = new List<Recuperacion_Contrasena>();
}
