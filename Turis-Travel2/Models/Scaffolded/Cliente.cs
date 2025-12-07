using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

public partial class Cliente
{
    [Key]
    public int ID_cliente { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string? Cedula { get; set; }

    [StringLength(50)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_registro { get; set; }

    [InverseProperty("ID_clienteNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
