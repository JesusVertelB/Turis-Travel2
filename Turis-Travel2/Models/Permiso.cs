using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public int IdRol { get; set; }

    public int IdModulo { get; set; }

    public int? EstadoPermiso { get; set; }

    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
