﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KrpanoCMS;
using KrpanoCMS.Rename;

namespace KrpanoCMS.Administration.Controllers
{
    public class PanoramaController : Controller
    {
        private Entities db = new Entities();

        // GET: Panorama
        public ActionResult Index()
        {
            return View(db.Panorama.ToList());
        }

        // GET: Panorama/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panorama panorama = db.Panorama.Find(id);
            if (panorama == null)
            {
                return HttpNotFound();
            }
            return View(panorama);
        }

        // GET: Panorama/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panorama/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Panorama panorama, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                string extension = Path.GetExtension(photo.FileName);
                FileUploader.Upload(photo, panorama.Id + extension);
               // Utils.UploadPhoto(photo);
                panorama.PictureUrl = photo.FileName;
            }

            if (ModelState.IsValid)
            {
                db.Panorama.Add(panorama);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(panorama);
        }

        // GET: Panorama/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panorama panorama = db.Panorama.Find(id);
            if (panorama == null)
            {
                return HttpNotFound();
            }
            return View(panorama);
        }

        // POST: Panorama/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserId,AddedOn,PictureUrl")] Panorama panorama)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panorama).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(panorama);
        }

        // GET: Panorama/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panorama panorama = db.Panorama.Find(id);
            if (panorama == null)
            {
                return HttpNotFound();
            }
            return View(panorama);
        }

        // POST: Panorama/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Panorama panorama = db.Panorama.Find(id);
            db.Panorama.Remove(panorama);
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
