using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace ProyectoDistribuidora.Data
{
    public class Email
    {
        public void  enviar(string receptor, string urlFirma, string cuerpo, string asunto)
        {
            try
            {
                /*Se crea una isntancia de un objeto email*/
                MailMessage email = new MailMessage();

                /*Agregar los destinatarios*/
                /*Email del administrador*/
                email.To.Add(new MailAddress("distribuidoraucr@gmail.com"));
                /*Minuto una hora 19  */
                email.To.Add(new MailAddress(receptor));

                //Se agrega al emisor
                email.From = new MailAddress("distribuidoraucr@gmail.com");

                //Asunto email
                email.Subject = asunto;

                //Se contruye la vista del email.
                string html = cuerpo;

                //Se indica que el contenido es HTML
                email.IsBodyHtml = true;

                //Se indica la prioridad del email
                email.Priority = MailPriority.Normal;

                //Aquí se crea el adjunto de la fotografía del sitio como firma
                Attachment attachment = new Attachment(urlFirma);

                //Se crear la etiqueta img para agregar la imagen como firma al body del email
                html += "<br><br><img src:'cid:imagen'/>";
                
                //Se crea la instancia para la vista html del body del email.
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);

                //Se crea la instancia del objeto inscrutado como una imagen de archivo adjunto
                LinkedResource img = new LinkedResource(urlFirma, MediaTypeNames.Image.Jpeg);

                //Se indica el ID de la imagen
                img.ContentId = "imagen";

                //Se adjunta la imagen
                view.LinkedResources.Add(img);

                //Se agrega la vista al email
                email.AlternateViews.Add(view);

                //Se instancia un object SmtpCient
                SmtpClient stmp = new SmtpClient();

                //Se indica el servidor de correo a implementar
                stmp.Host = "smtp.gmail.com";

                //El puerto de comunicación 
                stmp.Port = 587;

                //Se indica si utiliza seguridad tipo ssl
                stmp.EnableSsl = true;

                //Se indica si tenemos credenciales ahí por defautl 
                //en este caso no
                stmp.UseDefaultCredentials = false;

                //Aqui indicamos las credenciales del buzon
                stmp.Credentials = new NetworkCredential("distribuidoraucr@gmail.com", "ValeLuna.21!");
                //Se envia el imail
                stmp.Send(email);

                //Se liberan los recursos
                email.Dispose();
                stmp.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
