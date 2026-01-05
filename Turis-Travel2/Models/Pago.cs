using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turis_Travel2.Models
{
    public class Pago
    {
        [Key]
        [Column("ID_pago")]
        public int IdPago { get; set; }

        [Column("ID_reserva")]
        public int IdReserva { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPago { get; set; } = string.Empty; // 👈 CLAVE

        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }

        [StringLength(20)]
        public string Estado { get; set; } = "pagado"; // 👈 CLAVE

        public DateTime FechaPago { get; set; } = DateTime.Now; // 👈 CLAVE

        // 🔗 Relación (puede ser null hasta que EF la cargue)
        public Reserva? Reserva { get; set; } // 👈 CLAVE
    }
}
