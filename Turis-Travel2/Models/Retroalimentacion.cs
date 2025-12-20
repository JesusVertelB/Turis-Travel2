using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Retroalimentacion
{
    public int IdRetroalimentacion { get; set; }

    public int IdReserva { get; set; }

    public string? Comentario { get; set; }

    public int? Puntuacion { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Anonimo { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
