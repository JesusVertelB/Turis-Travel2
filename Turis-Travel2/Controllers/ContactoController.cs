using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;

public class ContactoController : Controller
{
    // Acción para mostrar la página de contacto
    public IActionResult Index()
    {
        return View();
    }

    // Acción para enviar el mensaje
    [HttpPost]
    public async Task<IActionResult> EnviarMensaje(string Nombre, string Correo, string Asunto, string Mensaje)
    {
        try
        {
            // Crear el correo
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("felipeagudelo708@gmail.com"); // tu correo real
            mail.To.Add("felipeagudelo708@gmail.com"); // correo destino
            mail.Subject = Asunto;
            mail.Body = $"Nombre: {Nombre}\nCorreo del remitente: {Correo}\nMensaje:\n{Mensaje}";

            // Configurar SMTP para Gmail
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("felipeagudelo708@gmail.com", "xnlf afnk tcdm yqsv");
            smtp.EnableSsl = true;

            // Enviar el correo
            await smtp.SendMailAsync(mail);

            TempData["MensajeOk"] = "Mensaje enviado con éxito.";
        }
        catch (System.Exception ex)
        {
            TempData["MensajeError"] = "Error al enviar el mensaje: " + ex.Message;
        }

        return RedirectToAction("Index");
    }
}



