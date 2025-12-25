namespace Turis_Travel2.Models
{
    public class DestinosPaquetesViewModel
    {
        public Destino Destinos { get; set; } 
        public List<PaquetesTuristicos> Paquetes { get; set; } = new List<PaquetesTuristicos>();
    }
}
