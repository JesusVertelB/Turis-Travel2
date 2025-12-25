using System;
using System.Collections.Generic;

namespace Turis_Travel2.Models;

public partial class Destino
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Pais { get; set; }

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string? Categoria { get; set; }

    public string? ImagenUrl { get; set; }

    public sbyte? Estado { get; set; }

    public virtual ICollection<PaquetesTuristicos> IdPaquete { get; set; } = new List<PaquetesTuristicos>();
}
