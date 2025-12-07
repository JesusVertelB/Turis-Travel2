using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_proveedor", Name = "ID_proveedor")]
public partial class Contrato
{
    [Key]
    public int ID_contrato { get; set; }

    public int ID_proveedor { get; set; }

    public DateOnly Fecha_inicio { get; set; }

    public DateOnly Fecha_fin { get; set; }

    [Column(TypeName = "text")]
    public string? Condiciones { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("ID_proveedor")]
    [InverseProperty("Contratos")]
    public virtual Proveedore ID_proveedorNavigation { get; set; } = null!;
}
