using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Models.Scaffolded;

public partial class Reporte
{
    [Key]
    public int ID_reporte { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha_generacion { get; set; }

    [Column(TypeName = "text")]
    public string? Datos { get; set; }
}
