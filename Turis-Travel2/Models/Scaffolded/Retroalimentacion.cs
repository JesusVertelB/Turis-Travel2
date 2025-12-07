using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Table("Retroalimentacion")]
[Index("ID_reserva", Name = "ID_reserva")]
public partial class Retroalimentacion
{
    [Key]
    public int ID_retroalimentacion { get; set; }

    public int ID_reserva { get; set; }

    [Column(TypeName = "text")]
    public string? Comentario { get; set; }

    public int? Puntuacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    public int? Anonimo { get; set; }

    [ForeignKey("ID_reserva")]
    [InverseProperty("Retroalimentacions")]
    public virtual Reserva ID_reservaNavigation { get; set; } = null!;
}
