using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class AsignacionConductore
{
    public int IdAsignacion { get; set; }

    public int? IdConductor { get; set; }

    public int? IdTransporte { get; set; }

    public DateOnly? FechaAsignacion { get; set; }

    public virtual Conductore? IdConductorNavigation { get; set; }

    public virtual Transporte? IdTransporteNavigation { get; set; }
}
