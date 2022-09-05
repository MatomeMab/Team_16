using Nazox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;
using System.Dynamic;

namespace Nazox.Controllers.Auth
{
    public class AuthRegisterController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: AuthRegister
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterUser(FormCollection form)
        {
            string EmailAddress = "";
            string FirstName = "";
            string Surname = "";
            string ContactNumber = "";
            string UserPassword = "";
            string Address = "";

            if (form["useremail"] != null && form["useremail"] != "")
            {
                EmailAddress = form["useremail"];
            }
            else
            {
                TempData["error"] = "Email Address field is required";
                return RedirectToAction("Index");
            }

            if (form["FirstName"] != null && form["FirstName"] != "")
            {
                FirstName = form["FirstName"];
            }
            else
            {
                TempData["error"] = "FirstName field is required";
                return RedirectToAction("Index");
            }

            if (form["Surname"] != null && form["Surname"] != "")
            {
                Surname = form["Surname"];

            }
            else
            {
                TempData["error"] = "Surname field is required";
                return RedirectToAction("Index");
            }

            if (form["ContactNumber"] != null && form["ContactNumber"] != "")
            {
                ContactNumber = form["ContactNumber"];
            }
            else
            {
                TempData["error"] = "Contact number field is required";
                return RedirectToAction("Index");
            }

            if (form["userpassword"] != null && form["userpassword"] != "")
            {
                UserPassword = form["UserPassword"];
            }
            else
            {
                TempData["error"] = "Password field is required";
                return RedirectToAction("Index");
            }

            if (form["Address"] != null && form["Address"] != "")
            {
                Address = form["Address"];
            }
            else
            {
                TempData["error"] = "Address field is required";
                return RedirectToAction("Index");
            }
            /*OTP*/
            Random generator = new Random();
            string generateOTP = generator.Next(1000, 9999).ToString();

            /*OTP*/
            var checkUserExists = db.Users.FirstOrDefault(a => a.Email == EmailAddress.Trim());

            if (checkUserExists != null)
            {
                TempData["UserExist"] = "The user account already exist";
                return RedirectToAction("Index");
            }

            User tblUser = new User();
            tblUser.Role_ID = 3;
            tblUser.OTP = generateOTP;
            tblUser.Active = false;
            tblUser.Email = EmailAddress;
            tblUser.Password = Hash(UserPassword);
            db.Users.Add(tblUser);
            await db.SaveChangesAsync();

            //New Client
            Client client = new Client();

            client.User_ID = tblUser.User_ID;
            client.ClientName = FirstName;
            client.ClientSurname = Surname;
            client.PhoneNum = ContactNumber;
            client.Address = Address;

            db.Clients.Add(client);
            await db.SaveChangesAsync();

            //User Role
            UserRole userRole = new UserRole();
            userRole.User_ID = tblUser.User_ID;
            userRole.RoleTypeId = 3;

            db.UserRoles.Add(userRole);
            await db.SaveChangesAsync();

            string Subject = "WENDA Logistics Group Account";
            string Body = "Hi New User</br> welcome to WENDA Logistics Group. User new account has been created please login"+tblUser.OTP;

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

            TempData["Success"] = "Account created successfully, please login";

            return RedirectToAction("Index", "AuthLogin", new { id = tblUser.User_ID });
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

        [HttpPost]
        public dynamic VerifyOTP(string OTP, string Email)
        {
            User findUser = db.Users.Where(zz => zz.Email == Email).FirstOrDefault();

            if (findUser.OTP == OTP)
            {
                findUser.OTP = null;
                findUser.Active = true;


                db.SaveChanges();

                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "success";
                return toReturn;
            }
            else
            {
                return "fail";
            }
        }
    }
}