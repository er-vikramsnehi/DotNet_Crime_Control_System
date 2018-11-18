using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using cmspro.Controllers;
using cmspro.Models;

namespace cmspro.Areas.Manager.Controllers
{
    public class ManagerController : Controller
    {
        private CMSEntities db = new CMSEntities();
         
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        public ActionResult SideBar()
        {
            return PartialView("SideBar");
        }

        public ActionResult TopBar()
        {

            return PartialView("TopBar");
        }

        public ActionResult DashBoard()
        {
            return PartialView("DashBoard");
        }
         
        public ActionResult VeriFyUser()
        {
           return View(db.users.ToList());
        }

        public ActionResult ActivateNow(int? id)
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
        public ActionResult ActivateNow([Bind(Include = "user_id,user_name,user_email,user_pwd,user_image,user_epwd,user_type,user_active,user_mobile")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult PoliceWork()
        {
             
            List <user> police = db.users.Where(x => x.user_type == "POLICE").ToList();

            ViewBag.PoliceList = new SelectList(police, "user_id", "user_id");
             
            return PartialView("PoliceWork");
        }

        public ActionResult FndName(ViewModel model)
        {
            
            List<user> us = db.users.Where(x => x.user_id == model.user_id).ToList();

            ViewBag.PoliceName = new SelectList(us, "user_name", "user_name");

            return View("PoliceName");
        }

        public ActionResult Report()
        {
            return View(db.reports.ToList());
        }

        public ActionResult ReportDetails(int? id)
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

        public ActionResult ApprovedFIR(int? id)
        {
           


            List<user> police = db.users.Where(x => x.user_type == "POLICE").ToList();

            ViewBag.PoliceList = new SelectList(police, "user_id", "user_id");


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


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirApproved([Bind(Include = "fir_approved_id,fir_title,fir_id,police_id,approve")] firapproved firapproved)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firapproved).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firapproved);
        }



        public ActionResult FirApproved(int? id)
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



        // GET: Manager/firpolice/Create
        public ActionResult FIRPolice()
        {
            return View();
        }

        // POST: Manager/firpolice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult FIRPolice(report model)
        {
            firpolouse obj = new firpolouse();


            try
            {
                obj.approvedfir_email = model.reporter_email;
                obj.approvedfir_police_id = model.police_id;
                obj.approvedfir_mobile = model.reporter_mobile;
                obj.approvedfir_title = model.fir_title;
                obj.approved_fir = model.fir_id.ToString();

                bool result = false;
                string email_subject = "Police is Approved your request.";
                string email_message = "<p style='color:red;font-size:50px;font-weight:bold;'><b>The Ploice ID  :</b>" + obj.approvedfir_police_id + ", Who is comming at your address , Please wait on there .<br /><hr/> <b>You F.I.R. For</b>" + obj.approvedfir_title + ",<br /><hr/>  in <b>City:</b>" + obj.approvedfir_mobile + ",<br /><hr/> <b> Email :</b>" + obj.approvedfir_email + ",<br /><hr/> <b> Your Report: </b>" + obj.approved_fir + "<br /><hr/> <b style='color:red;font-size:50px;font-weight:bold;'>Please be on that address</b></p>";

                result = SendEmail(obj.approvedfir_email, email_subject, email_message);


                db.firpolice.Add(obj);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }



            return RedirectToAction("Index");
        }







        public ActionResult CreatePolice()
        {
            return View();
        }

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









        public JsonResult SendMail(ViewModel model)
        {
            invite obj = new invite();
            CMSEntities db = new CMSEntities();
            bool result = false;
            result = SendEmail(model.email_invite, model.email_subject, model.email_message);


            try
            {

                obj.email_invite = model.email_invite;
                obj.email_subject = model.email_subject;
                obj.email_message = model.email_message;

                db.invites.Add(obj);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }







            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmail(string toEmail, string subject, string emailbody)
        {
            try
            {
                string smtpUserEmail = System.Configuration.ConfigurationManager.AppSettings["smtpUserEmail"].ToString();

                string smtpPassword = System.Configuration.ConfigurationManager.AppSettings["smtpPassword"].ToString(); ;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUserEmail, smtpPassword);

                MailMessage mailMessage = new MailMessage(smtpUserEmail, toEmail, subject, emailbody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ActionResult SendMail2(ViewModel model)
        {
            bool result = false;
            result = SendEmail2(model.email_invite, model.email_subject, model.email_message);

            return RedirectToAction("Index");
        }

        public ActionResult ViewMails()
        {

            return View();
        }

        public bool SendEmail2(string toEmail, string subject, string emailbody)
        {
            try
            {
                string smtpUserEmail = System.Configuration.ConfigurationManager.AppSettings["smtpUserEmail"].ToString();

                string smtpPassword = System.Configuration.ConfigurationManager.AppSettings["smtpPassword"].ToString(); ;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUserEmail, smtpPassword);

                MailMessage mailMessage = new MailMessage(smtpUserEmail, toEmail, subject, emailbody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
































        // GET: Manager/Manager/Details/5
        public ActionResult Details(int? id)
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

        // GET: Manager/Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Manager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_name,user_email,user_pwd,user_image,user_epwd,user_type,user_active,user_mobile")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Manager/Manager/Edit/5
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

        // POST: Manager/Manager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Manager/Manager/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Manager/Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
