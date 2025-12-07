using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Table("Recuperacion_Contrasena")]
[Index("ID_usuario", Name = "ID_usuario")]
[Index("Token", Name = "Token", IsUnique = true)]
public partial class Recuperacion_Contrasena
{
    [Key]
    public int ID_recuperacion { get; set; }

    public int ID_usuario { get; set; }

    [StringLength(250)]
    public string Token { get; set; } = null!;

    public DateOnly Expiracion { get; set; }

    public int? Usado { get; set; }

    [ForeignKey("ID_usuario")]
    [InverseProperty("Recuperacion_Contrasenas")]
    public virtual Usuario ID_usuarioNavigation { get; set; } = null!;
}
