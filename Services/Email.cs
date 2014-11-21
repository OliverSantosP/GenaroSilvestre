using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mime;
using System.Net.Mail;
using System.Web.Configuration;
using GenaroSilvestre.Models;

namespace GenaroSilvestre.Services
{
    public class Mail
    {
        public static void SendEmail(ContactModels Contacto)
        {
            try
            {
                MailMessage mailMsg = new MailMessage();
                string FormRecipientEmail = WebConfigurationManager.AppSettings["FormRecipientEmail"];
                string FormRecipientName = WebConfigurationManager.AppSettings["FormRecipientName"];
                string FormSenderEmail = WebConfigurationManager.AppSettings["FormSenderEmail"];
                string FormSenderName = WebConfigurationManager.AppSettings["FormSenderName"];
                string FormSubject = WebConfigurationManager.AppSettings["FormSubject"];

                mailMsg.To.Add(new MailAddress(FormRecipientEmail, FormRecipientName));
                mailMsg.From = new MailAddress(FormSenderEmail, FormSenderName);
                mailMsg.Subject = FormSubject;
                string text = "Ha recibido un mensaje nuevo desde GenaroSilvestre.com. \n\n Nombre: " + Contacto.Name + "\n Dirección: " + Contacto.Address + "\n Email: " + Contacto.Email + "\n Telefono: " + Contacto.Phone + "\n Ciudad: " + Contacto.City + "\n Comentario: " + Contacto.Comment;

                string SmtpClient = WebConfigurationManager.AppSettings["SmtpClient"];
                string SmtpClientUser = WebConfigurationManager.AppSettings["SmtpClientUser"];
                string SmtpClientPassword = WebConfigurationManager.AppSettings["SmtpClientPassword"];

                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));

                SmtpClient smtpClient = new SmtpClient(SmtpClient, Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpClientUser, SmtpClientPassword);

                smtpClient.Credentials = credentials;
                smtpClient.Send(mailMsg);
            }
            catch (Exception)
            {
                throw;
            } 
        }

       
    }
}