using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Conductore
{
    public int IdConductor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Licencia { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<AsignacionConductore> AsignacionConductores { get; set; } = new List<AsignacionConductore>();
}
