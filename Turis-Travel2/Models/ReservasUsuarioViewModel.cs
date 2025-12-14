namespace Turis_Travel2.Models
{
    public class ReservasUsuarioViewModel
    {
        public string Nombre { get; set; } = "";
        public int ViajesProximos { get; set; }
        public int MillasTotales { get; set; }
        public int ViajesCompletados { get; set; }

        public List<ReservaItemViewModel> Reservas { get; set; } = new();
    }

    public class ReservaItemViewModel
    {
        public string Destino { get; set; } = "";
        public string Fecha { get; set; } = "";
        public decimal Precio { get; set; }
        public string Estado { get; set; } = "";
    }
}
