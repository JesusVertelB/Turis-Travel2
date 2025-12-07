using System.ComponentModel.DataAnnotations;
using Turis_Travel2.Models.Scaffolded;

namespace Turis_Travel2.Models
{
    public class UsuariosViewModel 
    {
        [Required]
        public List<Reserva> Reserva { get; set; }
        [Required]
        public List<Usuario> Usuario { get; set; }
    }
}
