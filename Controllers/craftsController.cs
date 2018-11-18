using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using cmspro.Controllers;
using cmspro.Models;

namespace Youtube.Areas.Admin.Controllers
{
    public class craftsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Admin/crafts
        [ValidateInput(false)]
        public ActionResult Index()
        {
            craft obj = new craft();

            StringBuilder sb = new StringBuilder();
            sb.Append(HttpUtility.HtmlEncode(obj.craft_specification));


            obj.craft_specification = sb.ToString();
            string SEname = HttpUtility.HtmlEncode(obj.craft_specification);
            obj.craft_specification = SEname;

            return View(db.crafts.ToList());

        }

        // GET: Admin/crafts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            craft craft = db.crafts.Find(id);
            if (craft == null)
            {
                return HttpNotFound();
            }
            return View(craft);
        }






        // GET: Admin/crafts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/crafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
  

        // GET: Admin/crafts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            craft craft = db.crafts.Find(id);
            if (craft == null)
            {
                return HttpNotFound();
            }
            return View(craft);
        }

        // POST: Admin/crafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "craft_id,craft_name,craft_size,craft_weight,craft_specification,craft_image")] craft craft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(craft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(craft);
        }

        // GET: Admin/crafts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            craft craft = db.crafts.Find(id);
            if (craft == null)
            {
                return HttpNotFound();
            }
            return View(craft);
        }

        // POST: Admin/crafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            craft craft = db.crafts.Find(id);
            db.crafts.Remove(craft);
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
