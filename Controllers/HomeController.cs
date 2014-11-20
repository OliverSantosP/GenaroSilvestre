using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenaroSilvestre.Models;
using System.Net.Mime;
using System.Net.Mail;
using System.Web.Configuration;

namespace GenaroSilvestre.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AreasDePractica()
        {
            ViewBag.Message = "Areas de Practica.";
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Contactos.";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels Contacto)
        {

            if (ModelState.IsValid)
            {
                var Name = Contacto.Name;
                var Address = Contacto.Address;
                var Email = Contacto.Email;
                var Phone = Contacto.Phone;
                var City = Contacto.City;
                var Comment = Contacto.Comment;
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

                }
                ViewBag.ContactMessage = "Se ha enviado su mensaje.";
                return View();
            }
            else
            {
                ViewBag.Message = "Contactos.";
                return View();
            }

        }

        public ActionResult Perfil()
        {
            ViewBag.Message = "Perfil Ejecutivo.";

            return View();
        }
    }
}