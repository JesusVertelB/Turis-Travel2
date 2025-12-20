using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class RecuperacionContrasena
{
    public int IdRecuperacion { get; set; }

    public int IdUsuario { get; set; }

    public string Token { get; set; } = null!;

    public DateOnly Expiracion { get; set; }

    public int? Usado { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
