namespace Turis_Travel2.Models
{
    public class HomeUsuarioViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string Membresia { get; set; } = string.Empty;
        public int Puntos { get; set; }

        public int DestinosGuardados { get; set; }
        public int AlertasPrecio { get; set; }

        public string DestinoActual { get; set; } = string.Empty;
        public string FechasViaje { get; set; } = string.Empty;
        public string Viajeros { get; set; } = string.Empty;
        public string EstadoViaje { get; set; } = string.Empty;
    }
}

