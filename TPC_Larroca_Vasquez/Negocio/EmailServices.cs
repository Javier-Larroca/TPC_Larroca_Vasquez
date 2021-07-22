using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Dominio;

namespace Negocio
{
    class EmailServices
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailServices()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("clinicalave@gmail.com", "lave123456");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void correoNotificacionTurno(Paciente paciente, Turno turno)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@clinicalave.com");
            email.To.Add(paciente.Mail);
            email.Subject = "Notificacion de nuevo turno - LAVE";
            email.IsBodyHtml = true;
            email.Body = "<h1>¡Se ha registrado un nuevo turno con exito!</h1> " +
                         "<br>Hola," + paciente.Nombre + " te notificacamos desde Clinica LAVE que se registro un nuevo turno correctamente." +
                         "<br>Fecha y hora: " + turno.FechaTurno+
                         "<br>Recuerde concurrir al mismo con documento de identidad"+
                         "<h6>En caso de solicitar cancelar el mismo, notifiquelo cuanto antes.</h6>";

            //Faltaria ver el tema de especialidad y medico.

        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
