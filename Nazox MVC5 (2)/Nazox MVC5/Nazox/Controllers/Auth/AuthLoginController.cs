using Nazox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;
using System.Web.Security;
using System.Dynamic;

namespace Nazox.Controllers.Auth
{
    public class AuthLoginController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: AuthLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(FormCollection form)
        {
            /*OTP*/

            string Session = "12345";

            var emailAddress = form["EmailAddress"].Trim();
            var password = Hash(form["userpassword"].Trim());

            var tblUser = db.Users.FirstOrDefault(a => a.Email == emailAddress);
            bool validPass = false;

            if (tblUser != null)
            {
                if (tblUser.Password == password)
                {
                    validPass = true;
                    //tblUser.Active = true;
                    if (tblUser != null && validPass == true)
                    {
                        if (tblUser.Active == true)
                        {
                            TempData["inActive"] = "This user is not active. Contact the administrator";
                        }

                    }
                    else
                    tblUser.SessionID = Session;
                    db.Entry(tblUser).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Dashboard", new { id = tblUser.User_ID });

                }
                
                else
                {
                    TempData["errorMsg"] = "Something went wrong please try again. Please check your password/email";

                    return RedirectToAction("Index");
                }
            }

            TempData["errorMsg"] = "Something went wrong please try again";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(FormCollection form)
        {
            var emailAddress = form["useremail"].Trim();

            var tblUser = db.Users.FirstOrDefault(a => a.Email == emailAddress);

            if (tblUser != null)
            {
                var verifyUrl = "/AuthLogin/NewPassword";
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                
                string Subject = "WENDA Logistics Group : Account recovery";
                string Body = "Hi we received a request to reset your password. Please follow the link to reset your password " + link;

                string from = "wendalogisticsgroup@zohomail.com";
                string passwod = "Wendalogistics_123";
                SmtpClient smpt = new SmtpClient();
                smpt.Connect("smtp.zoho.com", 587, SecureSocketOptions.StartTls);
                smpt.Authenticate(from, passwod);
                smpt.AuthenticationMechanisms.Remove("XOAUTH2");


                using (var message = new MailMessage("wendalogisticsgroup@zohomail.com", tblUser.Email.Trim())
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                })
                    smpt.Send((MimeKit.MimeMessage)message);

                TempData["success"] = "Account recovery will be sent to your email address with a link to reset password";

                return RedirectToAction("Index", "AuthRecoverpw");

            }

            TempData["errorMsg"] = "Something went wrong please try again";

            return RedirectToAction("Index", "AuthRecoverpw");
        }


        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewPassword(FormCollection form, User user)
        {

            var emailAddress = form["EmailAddress"].Trim();
            var password = form["password"].Trim();

            var tblUser = db.Users.FirstOrDefault(a => a.Email == emailAddress);

            if (tblUser != null)
            {

                tblUser.Password = Hash(password);
                db.Entry(tblUser).State = EntityState.Modified;
                await db.SaveChangesAsync();

                TempData["Success"] = "Account Recovered please login";

                return RedirectToAction("Index");

            }

            TempData["errorMsg"] = "Something went wrong please try again";

            return RedirectToAction("Index", "AuthRecoverpw");

        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );

        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout(int?id)
        {
            //int id = 1;
            var tblUser = db.Users.FirstOrDefault(a => a.User_ID == id);
           // User tblUser = new User();
            tblUser.SessionID = null;
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index","AuthLogin");
        }


        //[HttpGet]
        //public object Logout(string token)
        //{
        //    using (DbContextTransaction transaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            User user = db.Users.Where(x => x.GUID == token).FirstOrDefault();
        //            if (user != null)
        //            {
        //                user.GUID = null;
        //                db.SaveChanges();
        //                transaction.Commit();
        //                dynamic toReturn = new ExpandoObject();
        //                toReturn.Message = ("You have been logged out.");
        //                return toReturn;
        //            }
        //            else
        //            {
        //                dynamic toReturn = new ExpandoObject();
        //                toReturn.Error = ("You are already logged out. Please refresh the page.");
        //                return BadRequest(toReturn.Error);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            transaction.Rollback();
        //            dynamic toReturn = new ExpandoObject();
        //            toReturn.Error = (e.Message);
        //            return BadRequest(toReturn.Error);
        //        }
        //    }
        //}


    }
}