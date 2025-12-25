// Archivo: ReservasUsuarioController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class ReservasUsuarioController : Controller

    {
        public IActionResult ConfigurarViaje(int IdDestino)
        {
            return View();
        }

        public IActionResult Index()
        {
            var nombre = User.Identity?.Name ?? "Usuario";

            var model = new ReservasUsuarioViewModel
            {
                Nombre = nombre,
                ViajesProximos = 3,
                MillasTotales = 14250,
                ViajesCompletados = 8,
                Reservas = new List<ReservaItemViewModel>
                {
                    new ReservaItemViewModel
                    {
                        Destino = "París, Francia",
                        Fecha = "12 Oct - 21 Oct, 2023",
                        Precio = 1200,
                        Estado = "Confirmado"
                    },
                    new ReservaItemViewModel
                    {
                        Destino = "Tokio, Japón",
                        Fecha = "05 Nov - 15 Nov, 2023",
                        Precio = 2450,
                        Estado = "Pendiente de pago"
                    },
                    new ReservaItemViewModel
                    {
                        Destino = "Cancún, México",
                        Fecha = "20 Dic - 27 Dic, 2023",
                        Precio = 900,
                        Estado = "Confirmado"
                    }
                }
            };

            return View(model);
        }


    }
}


