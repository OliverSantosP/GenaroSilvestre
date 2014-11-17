using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenaroSilvestre.Models;
using System.Net.Mime;
using System.Net.Mail;

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
                    mailMsg.To.Add(new MailAddress("oliversantosp@gmail.com", "Oliver Santos"));
                    mailMsg.To.Add(new MailAddress("srsamuelvasquez@gmail.com", "Samuel Vasquez"));
                    mailMsg.From = new MailAddress("genarosilvestre@gmail.com", "GenaroSilvestre.com");

                    mailMsg.Subject = "Nuevo mensaje desde GenaroSilvestre.com";
                    string text = "Ha recibido un mensaje nuevo desde GenaroSilvestre.com. \n\n Nombre: "+Contacto.Name+ "\n Dirección: "+ Contacto.Address+ "\n Email: "+Contacto.Email+ "\n Telefono: "+Contacto.Phone+ "\n Ciudad: "+ Contacto.City+ "\n Comentario: "+Contacto.Comment;
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));

                    SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("oliversantosp", "Claro2014*");
                    smtpClient.Credentials = credentials;
                    smtpClient.Send(mailMsg);
                }
                catch (Exception ex)
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