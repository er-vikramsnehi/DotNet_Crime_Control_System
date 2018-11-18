using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmspro.Models;

namespace cmspro.Areas.POLICE.Controllers
{
    public class PoliceController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: POLICE/Police
        public ActionResult Index()
        {
            return View(db.reports.ToList());
        }

        // GET: POLICE/Police/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report report = db.reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: POLICE/Police/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: POLICE/Police/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fir_id,fir_title,reporter_name,reporter_mobile,reporter_email,report_for,report_summary,report_image,report_processing,report_city,report_state,report_address")] report report)
        {
            if (ModelState.IsValid)
            {
                db.reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(report);
        }

        // GET: POLICE/Police/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report report = db.reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: POLICE/Police/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fir_id,fir_title,reporter_name,reporter_mobile,reporter_email,report_for,report_summary,report_image,report_processing,report_city,report_state,report_address")] report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: POLICE/Police/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report report = db.reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: POLICE/Police/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            report report = db.reports.Find(id);
            db.reports.Remove(report);
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
