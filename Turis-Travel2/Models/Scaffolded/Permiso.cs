using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_modulo", Name = "ID_modulo")]
[Index("ID_rol", Name = "ID_rol")]
public partial class Permiso
{
    [Key]
    public int ID_permiso { get; set; }

    public int ID_rol { get; set; }

    public int ID_modulo { get; set; }

    public int? Estado_permiso { get; set; }

    [ForeignKey("ID_modulo")]
    [InverseProperty("Permisos")]
    public virtual Modulo ID_moduloNavigation { get; set; } = null!;

    [ForeignKey("ID_rol")]
    [InverseProperty("Permisos")]
    public virtual Role ID_rolNavigation { get; set; } = null!;
}
