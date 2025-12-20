using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public int IdProveedor { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string? Condiciones { get; set; }

    public string? Estado { get; set; }

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
