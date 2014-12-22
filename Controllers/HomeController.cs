using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenaroSilvestre.Models;
using System.Net.Mime;
using System.Net.Mail;
using System.Web.Configuration;
using GenaroSilvestre.Services;

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

        public ActionResult NotariadoYLegalizacion()
        {
            ViewBag.Message = "Notariado y legalización.";
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

                GenaroSilvestre.Services.Mail.SendEmail(Contacto);
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