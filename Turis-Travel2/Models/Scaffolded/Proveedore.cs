using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("Nombre", Name = "idx_proveedor_nombre")]
public partial class Proveedore
{
    [Key]
    public int ID_proveedor { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string? Tipo { get; set; }

    [StringLength(100)]
    public string? Contacto { get; set; }

    [StringLength(250)]
    public string? Direccion { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [InverseProperty("ID_proveedorNavigation")]
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
