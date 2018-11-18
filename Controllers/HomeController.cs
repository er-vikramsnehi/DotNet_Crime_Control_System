using cmspro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace cmspro.Controllers
{
    public class HomeController : Controller
    {
        private CMSEntities db = new CMSEntities();


        public ActionResult Index()
        {
            List<usertype> list = db.usertypes.ToList();

            List<usertype> types = db.usertypes.ToList();


            ViewBag.TypeList = new SelectList(types, "role_user_type", "role_user_type");



            List<user> imglist = db.users.ToList();



            List<user> ImgList = imglist.Select(x => new user
            {
                user_id = x.user_id,
                user_image = x.user_image,
            }).ToList();

            return View();
        } 

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult CreateContact()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact([Bind(Include = "contact_id,contact_name,contact_mail,contact_mobile,contact_why")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }



        [HttpPost]
        public ActionResult LogOn(ViewModel model)
        {
            var password = EncriptionController.Encrypt(model.user_pwd, model.user_email);
            var email = model.user_email;

            using (CMSEntities db = new CMSEntities())
            {
                var v = db.users.Where(x => x.user_email == model.user_email & x.user_epwd == password).FirstOrDefault();
                if (v != null)
                {
                    Session["Active"] = v.user_active.ToString();
                    Session["id"] = v.user_id.ToString();
                    Session["name"] = v.user_name.ToString();
                    Session["email"] = v.user_email.ToString();
                    Session["image"] = v.user_image.ToString();
                    Session["mobile"] = v.user_mobile;
                    Session["accounttype"] = v.user_type.ToString();
                    Session["pwd"] = v.user_pwd.ToString();
                   

                }
                else
                {
                    Equals("Invalid Request");
                }
            }


            return RedirectToAction("Index");
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

                Session["image"] = obj.user_image;


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


                Session["id"] = obj.user_id;
                Session["name"] = obj.user_name;
                Session["email"] = obj.user_email;

                Session["mobile"] = obj.user_mobile;
                Session["accounttype"] = obj.user_type;
                Session["pwd"] = obj.user_pwd;
                Session["site"] = site.site_name;
                Session["id"] = site.user_id;
                Session["Active"] = obj.user_active;


            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index");
        }



        public ActionResult Sliders()
        {
            return PartialView("Sliders");
        }


        public ActionResult Chart()
        {
            return PartialView(db.reports.ToList());
        }
        public ActionResult ChartR()
        {
            return View(db.reports.ToList());
        }

        public ActionResult Chart2()
        {
            return PartialView("Chart2");
        }

        public ActionResult Clock()
        {
            return PartialView("Clock");
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

        public ActionResult Slider2()
        {

            return PartialView();
        }

        public ActionResult FIR()
        {
            return PartialView("FIR");
        }

        public ActionResult ReportSave(report model, HttpPostedFileBase report_image)
        {
            CMSEntities db = new CMSEntities();
            report obj = new report();
            Random r = new Random();
            int k = r.Next(200, 5000);

            try
            {
                if (report_image != null)
            {

                string filename = EncriptionController.Encrypt(Path.GetFileNameWithoutExtension(report_image.FileName) + k, model.reporter_email);
                string extention = Path.GetExtension(report_image.FileName);
                string filenamewithoutextention = Path.GetFileNameWithoutExtension(report_image.FileName);

                filename = filename + DateTime.Now.ToString("yymmssff") + extention;

                filename.Replace(@"/", "");
                filename.Replace(@"\", "");
                
                string image = EncriptionController.Encrypt(filename, model.reporter_email) + extention;
                string a = image.Replace(@"/", "");
                string b = a.Replace(@"\", "");



                report_image.SaveAs(Server.MapPath("~/uploads/" + b));
                obj.report_image = b;
 
            }
            

                
                obj.fir_title = model.fir_title;
                obj.reporter_name = model.reporter_name;
                obj.reporter_mobile = model.reporter_mobile;
                obj.reporter_email = model.reporter_email;
                obj.report_for = model.report_for;
                obj.report_summary = model.report_summary;
                obj.report_processing = "Pending";
                obj.report_city = model.report_city;
                obj.report_state = model.report_state;
                obj.report_address = model.report_address;


              

                bool result = false;
                string email_subject = "FIR Approved.";
                string email_message ="<p style='color:red;font-size:50px;font-weight:bold;'><b>Your F.I.R. is Registered By name :</b>" + obj.reporter_name + ",<br /><hr/> <b>You F.I.R. For</b>" + obj.report_for + ",<br /><hr/>  in <b>City:</b>" + obj.report_city + ",<br /><hr/> <b> State :</b>" + obj.report_state + ",<br /><hr/> <b> Your Report: </b>" + obj.report_summary + "<br /><hr/> <b style='color:red;font-size:50px;font-weight:bold;'>THE POLICE WILL GET YOU SOON</b></p>";

                result = SendEmail(obj.reporter_email, email_subject, email_message);


                db.reports.Add(obj);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index");

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }


        public ActionResult Army()
        {
           return PartialView("Army", db.armies.ToList());
        }


        public ActionResult DetailsArmy(int? id)
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





        public ActionResult Missile()
        {
           return PartialView("Missile", db.crafts.ToList());
        }


        public ActionResult DetailsCraft(int? id)
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



        // GET: armies/Create
        public ActionResult CreateArmy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArmy([Bind(Include = "army_id,army_name,army_email,army_mobile,army_posting,army_account_number,army_ifsc_code,army_image,army_wife,army_father,army_address,army_medal,army_counter_strike,army_hurt_count,army_summary")] army army)
        {
            if (ModelState.IsValid)
            {
                db.armies.Add(army);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(army);
        }

        public ActionResult ListArmy()
        {
            return PartialView("ListArmy", db.armies.ToList());

        }


        public ActionResult CreateForm()
        {
            List<usertype> list = db.usertypes.ToList();

            List<usertype> types = db.usertypes.ToList();


            ViewBag.TypeList = new SelectList(types, "role_user_type", "role_user_type");



            List<user> imglist = db.users.ToList();



            List<user> ImgList = imglist.Select(x => new user
            {
                user_id = x.user_id,
                user_image = x.user_image,
            }).ToList();

            return View();
        }

    }
}
