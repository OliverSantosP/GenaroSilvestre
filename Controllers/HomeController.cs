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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Home", "News");
        }

        public ActionResult AreasDePractica()
        {
            ViewBag.Message = Resources.Resources.PracticeAreasTitle;

            return View();
        }

        public ActionResult PerfilEjecutivo()
        {
            ViewBag.Message = Resources.Resources.ExecutiveProfileTitle;

            return View();
        }

        public ActionResult PerfilDeLaFirma()
        {
            ViewBag.Message = Resources.Resources.FirmProfileTitle;
            return View();
        }

        public ActionResult NotariadoYLegalizacion()
        {
            ViewBag.Message = Resources.Resources.NotaryTitle;
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = Resources.Resources.ContactTitle;
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
                ViewBag.ContactMessage = Resources.Resources.ContactMessageSent;
                return View();
            }
            else
            {
                ViewBag.Message = Resources.Resources.ContactTitle;
                return View();
            }

        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }

    }
}