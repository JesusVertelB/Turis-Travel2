using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Modulo
{
    public int IdModulo { get; set; }

    public string NombreModulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
