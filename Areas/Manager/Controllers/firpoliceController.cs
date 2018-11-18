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

namespace cmspro.Areas.Manager.Controllers
{
    public class firpoliceController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Manager/firpolice
        public async Task<ActionResult> Index()
        {
            return View(await db.firpolice.ToListAsync());
        }

        // GET: Manager/firpolice/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firpolouse firpolouse = await db.firpolice.FindAsync(id);
            if (firpolouse == null)
            {
                return HttpNotFound();
            }
            return View(firpolouse);
        }

        // GET: Manager/firpolice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/firpolice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "approvedfir_id,approvedfir_title,approvedfir_email,approvedfir_police_id,approvedfir_mobile,approved_fir")] firpolouse firpolouse)
        {
            if (ModelState.IsValid)
            {
                db.firpolice.Add(firpolouse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(firpolouse);
        }

        // GET: Manager/firpolice/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firpolouse firpolouse = await db.firpolice.FindAsync(id);
            if (firpolouse == null)
            {
                return HttpNotFound();
            }
            return View(firpolouse);
        }

        // POST: Manager/firpolice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "approvedfir_id,approvedfir_title,approvedfir_email,approvedfir_police_id,approvedfir_mobile,approved_fir")] firpolouse firpolouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firpolouse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(firpolouse);
        }

        // GET: Manager/firpolice/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firpolouse firpolouse = await db.firpolice.FindAsync(id);
            if (firpolouse == null)
            {
                return HttpNotFound();
            }
            return View(firpolouse);
        }

        // POST: Manager/firpolice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            firpolouse firpolouse = await db.firpolice.FindAsync(id);
            db.firpolice.Remove(firpolouse);
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
