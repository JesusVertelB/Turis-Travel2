using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Notificacione
{
    public int IdNotificacion { get; set; }

    public int? IdReserva { get; set; }

    public string? Tipo { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public string? Destinatario { get; set; }

    public string? Estado { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }
}
