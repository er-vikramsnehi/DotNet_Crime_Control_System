using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmspro.Models;

namespace cmspro.Areas.Officer.Controllers
{
    public class OfficerController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Officer/Officer
        public ActionResult Index()
        {
            return View(db.police.ToList());
        }

        // GET: Officer/Officer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police police = db.police.Find(id);
            if (police == null)
            {
                return HttpNotFound();
            }
            return View(police);
        }

        // GET: Officer/Officer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Officer/Officer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "police_id,police_name,police_mobile,police_email,police_type,police_idcard,police_station,police_state,police_city,police_home_address,police_activation")] police police)
        {
            if (ModelState.IsValid)
            {
                db.police.Add(police);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(police);
        }

        // GET: Officer/Officer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police police = db.police.Find(id);
            if (police == null)
            {
                return HttpNotFound();
            }
            return View(police);
        }

        // POST: Officer/Officer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "police_id,police_name,police_mobile,police_email,police_type,police_idcard,police_station,police_state,police_city,police_home_address,police_activation")] police police)
        {
            if (ModelState.IsValid)
            {
                db.Entry(police).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(police);
        }

        // GET: Officer/Officer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police police = db.police.Find(id);
            if (police == null)
            {
                return HttpNotFound();
            }
            return View(police);
        }

        // POST: Officer/Officer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            police police = db.police.Find(id);
            db.police.Remove(police);
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
