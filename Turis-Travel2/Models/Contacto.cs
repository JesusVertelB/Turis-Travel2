using System.ComponentModel.DataAnnotations;

namespace Turis_Travel2.Models
{
    public class Contacto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string Mensaje { get; set; }
    }
}

