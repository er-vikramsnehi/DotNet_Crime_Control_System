using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmspro.Models;

namespace cmspro.Areas.Manager.Controllers
{
    public class firapprovedsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Manager/firapproveds
        public ActionResult Index()
        {
            return View(db.firapproveds.ToList());
        }

        // GET: Manager/firapproveds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firapproved firapproved = db.firapproveds.Find(id);
            if (firapproved == null)
            {
                return HttpNotFound();
            }
            return View(firapproved);
        }

        // GET: Manager/firapproveds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/firapproveds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fir_approved_id,fir_title,fir_id,police_id,approve")] firapproved firapproved)
        {
            if (ModelState.IsValid)
            {
                db.firapproveds.Add(firapproved);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(firapproved);
        }

        // GET: Manager/firapproveds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firapproved firapproved = db.firapproveds.Find(id);
            if (firapproved == null)
            {
                return HttpNotFound();
            }
            return View(firapproved);
        }

        // POST: Manager/firapproveds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fir_approved_id,fir_title,fir_id,police_id,approve")] firapproved firapproved)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firapproved).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firapproved);
        }

        // GET: Manager/firapproveds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firapproved firapproved = db.firapproveds.Find(id);
            if (firapproved == null)
            {
                return HttpNotFound();
            }
            return View(firapproved);
        }

        // POST: Manager/firapproveds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            firapproved firapproved = db.firapproveds.Find(id);
            db.firapproveds.Remove(firapproved);
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
