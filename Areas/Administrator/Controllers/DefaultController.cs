using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using cmspro.Controllers;
using cmspro.Models;



namespace cmspro.Areas.Administrator.Controllers
{
    public class DefaultController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Admin/Default
        public ActionResult Index()
        {
            CMSEntities db = new CMSEntities();

            return View(db.users.ToList());
        }

        public ActionResult Delete(int? id)
        {
            CMSEntities db = new CMSEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CMSEntities db = new CMSEntities();

            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
 
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(ViewModel model, HttpPostedFileBase user_image)
        {
            CMSEntities db = new CMSEntities();

            user obj = new user();
            Site site = new Site();
            activate ac = new activate();
            Random r = new Random();
            int k = r.Next(200, 5000);


            if (user_image != null)
            {

                string filename = EncriptionController.Encrypt(Path.GetFileNameWithoutExtension(user_image.FileName) + k, model.user_email + model.user_pwd);
                string extention = Path.GetExtension(user_image.FileName);
                string filenamewithoutextention = Path.GetFileNameWithoutExtension(user_image.FileName);

                filename = filename + DateTime.Now.ToString("yymmssff") + extention;

                filename.Replace(@"/", "");
                filename.Replace(@"\", "");

                string image = EncriptionController.Encrypt(filename, model.user_email) + extention;
                string a = image.Replace(@"/", "");
                string b = a.Replace(@"\", "");



                user_image.SaveAs(Server.MapPath("~/uploads/" + b));


                obj.user_image = b;

             

            }

            try
            {


                obj.user_active = 0;

                obj.user_mobile = model.user_mobile;

                obj.user_name = model.user_name;
                obj.user_email = model.user_email;

                obj.user_pwd = model.user_pwd;

                string epwd = EncriptionController.Encrypt(model.user_pwd, model.user_email);

                obj.user_epwd = epwd;

                obj.user_type = model.user_type;


                db.users.Add(obj);

                db.SaveChanges();

                int LatestUser_id = obj.user_id;
                site.site_name = model.site_name;
                site.user_id = LatestUser_id.ToString();

                db.Sites.Add(site);



                ac.activate_account = "pending";
                ac.activate_user_id = LatestUser_id;

                db.activates.Add(ac);


                db.SaveChanges();


                 


            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index");
        }





        // GET: Administrator/users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_name,user_email,user_pwd,user_image,user_epwd,user_type,user_active,user_mobile")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
          
        public ActionResult Details(int? id)
        {
            CMSEntities db = new CMSEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
         
        protected override void Dispose(bool disposing)
        {
            CMSEntities db = new CMSEntities();

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SideBar()
        {
            return PartialView("SideBar");
        }

        public ActionResult ListUser()
        {
            CMSEntities db = new CMSEntities();

            return PartialView("ListUser", db.users.ToList());
        }

        public ActionResult TopBar()
        {

            return PartialView("TopBar");
        }

        public ActionResult DashBoard()
        {
            return PartialView("DashBoard");
        }




        public ActionResult CreatePolice()
        {
            return View();
        }

        // POST: Administrator/armies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePolice(army model, HttpPostedFileBase army_image)
        {

            CMSEntities db = new CMSEntities();

            army obj = new army();
            Site site = new Site();
            activate ac = new activate();
            Random r = new Random();
            int k = r.Next(200, 5000);


            if (army_image != null)
            {

                string filename = EncriptionController.Encrypt(Path.GetFileNameWithoutExtension(army_image.FileName) + k, model.army_email);
                string extention = Path.GetExtension(army_image.FileName);
                string filenamewithoutextention = Path.GetFileNameWithoutExtension(army_image.FileName);

                filename = filename + DateTime.Now.ToString("yymmssff") + extention;

                filename.Replace(@"/", "");
                filename.Replace(@"\", "");

                string image = EncriptionController.Encrypt(filename, model.army_email) + extention;
                string a = image.Replace(@"/", "");
                string b = a.Replace(@"\", "");



                army_image.SaveAs(Server.MapPath("~/uploads/" + b));


                obj.army_image = b;



            }

            try
            {

                 

                obj.army_account_number = model.army_account_number;

                obj.army_address = model.army_address;
                obj.army_counter_strike = model.army_counter_strike;

                obj.army_email = model.army_email;

               
                obj.army_father = model.army_father;

                obj.army_hurt_count = model.army_hurt_count;

                obj.army_ifsc_code = model.army_ifsc_code;
                obj.army_medal = model.army_medal;
                obj.army_mobile = model.army_mobile;
                obj.army_name = model.army_name;
                obj.army_posting = model.army_posting;
                obj.army_summary = model.army_summary;
                obj.army_wife = model.army_wife;



                db.armies.Add(obj);

                db.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index");
             
        }











        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCraft(craft model, HttpPostedFileBase craft_image)
        {

            CMSEntities db = new CMSEntities();
            craft cr = new craft();

            Random r = new Random();
            int k = r.Next(200, 5000);

            if (craft_image != null)
            {

                string filename = EncriptionController.Encrypt(Path.GetFileNameWithoutExtension(craft_image.FileName) + k, model.craft_name);
                string extention = Path.GetExtension(craft_image.FileName);
                string filenamewithoutextention = Path.GetFileNameWithoutExtension(craft_image.FileName);

                filename = filename + DateTime.Now.ToString("yymmssff") + extention;

                filename.Replace(@"/", "");

                string image = EncriptionController.Encrypt(filename, model.craft_name) + extention;
                image.Replace(@"/", "");



                craft_image.SaveAs(Server.MapPath("~/uploads/" + filename));


                cr.craft_image = filename;

            }

            try
            {

                cr.craft_name = model.craft_name;
                cr.craft_size = model.craft_size;


                string a = model.craft_specification.Replace(@"<script>", "@lt;script@gt;");
                string b = a.Replace(@"</script>", "@lt;/script@gt;");

                cr.craft_specification = b;
                cr.craft_weight = model.craft_weight;

                db.crafts.Add(cr);


                db.SaveChanges();


            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("./");



        }





        public ActionResult Craftlist()
        {
            CMSEntities db = new CMSEntities();
            craft obj = new craft();

            StringBuilder sb = new StringBuilder();
            sb.Append(HttpUtility.HtmlEncode(obj.craft_specification));


            obj.craft_specification = sb.ToString();
            string SEname = HttpUtility.HtmlEncode(obj.craft_specification);
            obj.craft_specification = SEname;

            return View(db.crafts.ToList());
        }


        public ActionResult DeleteCraft(int? id)
        {
            CMSEntities db = new CMSEntities();

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
        public ActionResult DeleteCraftConfirmed(int id)
        {
            CMSEntities db = new CMSEntities();
            craft craft = db.crafts.Find(id);
            db.crafts.Remove(craft);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


         




        public ActionResult EditCraft(int? id)
        {
            CMSEntities db = new CMSEntities();


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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCraft([Bind(Include = "craft_id,craft_name,craft_size,craft_weight,craft_specification")] craft craft, HttpPostedFileBase craft_image)
        {
            CMSEntities db = new CMSEntities();
            Random r = new Random();
            int k = r.Next(200, 5000);
            craft obj = new craft();

            if (ModelState.IsValid)
            {
                if (craft_image != null)
                {

                    string filename = EncriptionController.Encrypt(Path.GetFileNameWithoutExtension(craft_image.FileName) + k, craft.craft_name);
                    string extention = Path.GetExtension(craft_image.FileName);
                    string filenamewithoutextention = Path.GetFileNameWithoutExtension(craft_image.FileName);

                    filename = filename + DateTime.Now.ToString("yymmssff") + extention;

                    filename.Replace(@"/", "");


                    craft_image.SaveAs(Server.MapPath("~/uploads/" + filename));

                    obj.craft_name = filename;
                }

                db.Entry(craft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(craft);
        }





     public ActionResult AddPolice()
        {
            return View();
        }


    }
}