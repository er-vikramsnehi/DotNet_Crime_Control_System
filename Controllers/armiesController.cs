using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmspro.Models;

namespace Youtube.Controllers
{
    public class armiesController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: armies
        public async Task<ActionResult> Index()
        {
            return View(await db.armies.ToListAsync());
        }

        // GET: armies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            army army = await db.armies.FindAsync(id);
            if (army == null)
            {
                return HttpNotFound();
            }
            return View(army);
        }

        // GET: armies/Create
        public ActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "army_id,army_name,army_email,army_mobile,army_posting,army_account_number,army_ifsc_code,army_image,army_wife,army_father,army_address,army_medal,army_counter_strike,army_hurt_count,army_summary")] army army)
        {
            if (ModelState.IsValid)
            {
                db.armies.Add(army);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(army);
        }

        // GET: armies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            army army = await db.armies.FindAsync(id);
            if (army == null)
            {
                return HttpNotFound();
            }
            return View(army);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "army_id,army_name,army_email,army_mobile,army_posting,army_account_number,army_ifsc_code,army_image,army_wife,army_father,army_address,army_medal,army_counter_strike,army_hurt_count,army_summary")] army army)
        {
            if (ModelState.IsValid)
            {
                db.Entry(army).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(army);
        }

        // GET: armies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            army army = await db.armies.FindAsync(id);
            if (army == null)
            {
                return HttpNotFound();
            }
            return View(army);
        }

        // POST: armies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            army army = await db.armies.FindAsync(id);
            db.armies.Remove(army);
            await db.SaveChangesAsync();
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
