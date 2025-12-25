using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<PaquetesTuristicos> IdPaquetes { get; set; } = new List<PaquetesTuristicos>();
}
