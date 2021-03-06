﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenaroSilvestre.Models;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web.Configuration;

namespace GenaroSilvestre.Controllers
{
    public class NewsController : BaseController
    {
        private Model1Container db = new Model1Container();

        // GET: /News/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: /Home/
        public ActionResult Home()
        {
            return View(db.News.ToList());
        }

        // GET: /News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: /News/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,TitleEnglish,TextEnglish,Image,Created,Updated,User")] News news, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var image = file;
                if (image == null)
                {
                    ViewBag.UploadMessage = @Resources.Resources.ImageUploadFailed;
                }
                else
                {
                    
                    GenaroSilvestre.Models.Users MyUser = new GenaroSilvestre.Models.Users();
                    MyUser = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                    news.User = MyUser;
                    news.Created = System.DateTime.Now;
                    news.Updated = System.DateTime.Now;
                    news.Image = GenaroSilvestre.Services.Azure.UploadImage(file);
                    db.News.Add(news);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }

            return View(news);
        }

        // GET: /News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,TitleEnglish,TextEnglish,Image,Created,Updated")] News news, HttpPostedFileBase file)
        {
            news.Updated = System.DateTime.Now;
            GenaroSilvestre.Models.Users MyUser = new GenaroSilvestre.Models.Users();

            MyUser = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            news.User = MyUser;
            var image = file;
            if (image != null)
            {
                news.Image = news.Image = GenaroSilvestre.Services.Azure.UploadImage(file);
            }

            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: /News/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            GenaroSilvestre.Services.Azure.DeleteImage(news.Image);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
