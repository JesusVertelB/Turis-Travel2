using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

[Index("ID_itinerario", Name = "ID_itinerario")]
[Index("ID_paquete", Name = "ID_paquete")]
[Index("ID_transporte", Name = "ID_transporte")]
[Index("ID_cliente", Name = "idx_reserva_usuario")]
public partial class Reserva
{
    [Key]
    public int ID_reserva { get; set; }

    public int ID_cliente { get; set; }

    public int? ID_paquete { get; set; }

    public int? ID_itinerario { get; set; }

    public int? ID_transporte { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_solicitud { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    public int? Numero_pasajeros { get; set; }

    [Precision(10, 2)]
    public decimal? Precio_total { get; set; }

    [ForeignKey("ID_cliente")]
    [InverseProperty("Reservas")]
    public virtual Cliente ID_clienteNavigation { get; set; } = null!;

    [ForeignKey("ID_itinerario")]
    [InverseProperty("Reservas")]
    public virtual Itinerario? ID_itinerarioNavigation { get; set; }

    [ForeignKey("ID_paquete")]
    [InverseProperty("Reservas")]
    public virtual Paquetes_Turistico? ID_paqueteNavigation { get; set; }

    [ForeignKey("ID_transporte")]
    [InverseProperty("Reservas")]
    public virtual Transporte? ID_transporteNavigation { get; set; }

    [InverseProperty("ID_reservaNavigation")]
    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    [InverseProperty("ID_reservaNavigation")]
    public virtual ICollection<Retroalimentacion> Retroalimentacions { get; set; } = new List<Retroalimentacion>();
}
