using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmspro.Models;

namespace cmspro.Areas.Administrator.Controllers
{
    public class armiesController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Administrator/armies
        public ActionResult Index()
        {
            return View(db.armies.ToList());
        }

        // GET: Administrator/armies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            army army = db.armies.Find(id);
            if (army == null)
            {
                return HttpNotFound();
            }
            return View(army);
        }

        // GET: Administrator/armies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/armies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "army_id,army_name,army_email,army_mobile,army_posting,army_account_number,army_ifsc_code,army_image,army_wife,army_father,army_address,army_medal,army_counter_strike,army_hurt_count,army_summary")] army army)
        {
            if (ModelState.IsValid)
            {
                db.armies.Add(army);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return RedirectToAction("./");
        }

        // GET: Administrator/armies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            army army = db.armies.Find(id);
            if (army == null)
            {
                return HttpNotFound();
            }
            return View(army);
        }

        // POST: Administrator/armies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "army_id,army_name,army_email,army_mobile,army_posting,army_account_number,army_ifsc_code,army_image,army_wife,army_father,army_address,army_medal,army_counter_strike,army_hurt_count,army_summary")] army army)
        {
            if (ModelState.IsValid)
            {
                db.Entry(army).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(army);
        }

        // GET: Administrator/armies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            army army = db.armies.Find(id);
            if (army == null)
            {
                return HttpNotFound();
            }
            return View(army);
        }

        // POST: Administrator/armies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            army army = db.armies.Find(id);
            db.armies.Remove(army);
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
