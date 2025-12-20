using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Reporte
{
    public int IdReporte { get; set; }

    public string? Tipo { get; set; }

    public DateTime? FechaGeneracion { get; set; }

    public string? Datos { get; set; }
}
