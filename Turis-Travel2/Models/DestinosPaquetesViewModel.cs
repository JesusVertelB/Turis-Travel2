using System.Collections.Generic;
using Turis_Travel2.Models;

namespace Turis_Travel2.Models.ViewModels
{
    public class DestinosPaquetesViewModel
    {
        public Destino Destino { get; set; }
        public List<PaquetesTuristico> Paquetes { get; set; }

        public List<Retroalimentacion> Comentarios { get; set; } = new List<Retroalimentacion>();
    }
}
