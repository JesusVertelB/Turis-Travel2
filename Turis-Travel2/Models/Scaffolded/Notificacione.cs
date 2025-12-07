using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_reserva", Name = "ID_reserva")]
public partial class Notificacione
{
    [Key]
    public int ID_notificacion { get; set; }

    public int? ID_reserva { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_envio { get; set; }

    [StringLength(100)]
    public string? Destinatario { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("ID_reserva")]
    [InverseProperty("Notificaciones")]
    public virtual Reserva? ID_reservaNavigation { get; set; }
}
