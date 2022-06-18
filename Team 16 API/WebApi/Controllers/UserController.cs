using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/User")]
    public class UserController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            objEntity.Configuration.ProxyCreationEnabled = false;
            return objEntity.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await objEntity.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            objEntity.Users.Add(user);
            await objEntity.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.User_ID }, user);
        }

        [HttpPost]
        [Route("ActivateUser")]
        public dynamic ActivateUser(int UserID)
        {
            User findUser = objEntity.Users.Where(zz => zz.User_ID == UserID).FirstOrDefault();

            if (findUser.Active == true)
            {
                findUser.Active = false;
                objEntity.SaveChanges();
            }
            else
            {
                findUser.Active = true;
                objEntity.SaveChanges();
            }

            return "success";
        }


        [HttpPost]
        [Route("VerifyOTP")]
        public dynamic VerifyOTP(string OTP, int UserID)
        {
            User findUser = objEntity.Users.Where(zz => zz.User_ID == UserID).FirstOrDefault();

            if (findUser.OTP == OTP)
            {
                findUser.OTP = null;
                findUser.Active = true;


                objEntity.SaveChanges();

                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "success";
                return toReturn;
            }
            else
            {
                return "fail";
            }
        }

        [HttpPost]
        [Route("Login")]
        public dynamic Login(string Username, string Password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                dynamic toReturn = new ExpandoObject();
                User findUser = objEntity.Users.Where(zz => zz.Email == Username).FirstOrDefault();

                if (findUser == null)
                {

                    toReturn.Result = "Error";
                    toReturn.Message = "Incorrect username or password";
                    return toReturn;
                }

                string findHashedPassword = GenerateHash(Password);

                Password findPassword = objEntity.Passwords.Where(zz => zz.User_ID == findUser.User_ID).OrderByDescending(zz => zz.Date).FirstOrDefault();
                bool validPass = false;

                if (findPassword.Password1 == findHashedPassword)
                {
                    validPass = true;
                }

                if (findUser != null && validPass == true)
                {
                    if (findUser.Active == false)
                    {
                        toReturn.Result = "Error";
                        toReturn.Message = "This user is not active. Contact the administrator";
                        return toReturn;
                    }

                    Guid g = Guid.NewGuid();
                    findUser.SessionID = g.ToString();
                    objEntity.Entry(findUser).State = EntityState.Modified;

                    objEntity.SaveChanges();
                    toReturn.Result = "success";
                    string sesh = g.ToString();
                    toReturn.SessionID = sesh;
                    toReturn.RoleID = findUser.Role_ID;
                    return toReturn;

                }

                toReturn.Result = "Error";
                toReturn.Message = "Incorrect username or password";
                return toReturn;
            }
            catch (Exception e)
            {
                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "Error";
                toReturn.Message = "Incorrect username or password";
                return toReturn;
            }
        }

        [HttpPost]
        [Route("ResendOTP")]
        public dynamic ResendOTP(int UserID)
        {
            User findUser = objEntity.Users.Where(zz => zz.Role_ID == UserID).FirstOrDefault();

            Random generator = new Random();
            String generateOTP = generator.Next(100000, 999999).ToString();


            findUser.OTP = generateOTP;

            objEntity.SaveChanges();
            SendOTP(findUser.Email, findUser.OTP);

            return "success";

        }


        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_ID)
            {
                return BadRequest();
            }

            objEntity.Entry(user).State = EntityState.Modified;

            try
            {
                await objEntity.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("RegisterClient")]
        public dynamic RegisterClient([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Random generator = new Random();
                String generateOTP = generator.Next(1000, 9999).ToString();

                User findUser = objEntity.Users.Where(zz => zz.Email == user.Email).FirstOrDefault();

                List<Client> getClient = user.Clients.ToList();
                string ClientName = getClient[0].ClientName;
                string ClientSurname = getClient[0].ClientSurname;

                Client findClient = objEntity.Clients.Where(zz => zz.ClientName == ClientName && zz.ClientSurname == ClientSurname).FirstOrDefault();

                if (findUser != null || findUser != null)
                {

                    return "duplicate";
                }

                User newUser = new User
                {

                    //Debug.Assert(newUser!=null);
                    Email = user.Email,
                    Role_ID = user.Role_ID,
                    OTP = generateOTP
                };


                objEntity.Users.Add(newUser);
                objEntity.SaveChanges();

                int UserID = newUser.User_ID;
                var date = DateTime.Now;

                
                Password savePassword =user.Passwords.ElementAt(0);
                savePassword.Password1 = GenerateHash(savePassword.Password1);
                savePassword.Time = date.TimeOfDay;
                savePassword.Date = date.Date;
                savePassword.User_ID = UserID;


                Client newClient = user.Clients.ElementAt(0);
                newClient.User_ID = UserID;
                
                objEntity.Passwords.Add(savePassword);
                objEntity.Clients.Add(newClient);

                objEntity.SaveChanges();
                SendOTP(newUser.Email, newUser.OTP);

                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "success";
                toReturn.UserID = UserID;

                return toReturn;
            }
            catch (Exception err)
            {
                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "error";
                toReturn.Message = err.Message;


                return toReturn;
            }


        }

        [HttpPost]
        [Route("RegisterEmployee")]
        public dynamic RegisterEmployee([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Random generator = new Random();
                String generateOTP = generator.Next(1000, 9999).ToString();

                User findUser = objEntity.Users.Where(zz => zz.Email == user.Email).FirstOrDefault();

                List<Employee> getEmployee = user.Employees.ToList();
                string EmployeeName = getEmployee[0].EmployeeName;
                string EmployeeSurname = getEmployee[0].EmployeeSurname;

                Employee findEmployee = objEntity.Employees.Where(zz => zz.EmployeeName == EmployeeName && zz.EmployeeSurname == EmployeeSurname).FirstOrDefault();

                if (findUser != null || findUser != null)
                {

                    return "duplicate";
                }

                User newUser = new User
                {

                    //Debug.Assert(newUser!=null);
                    Email = user.Email,
                    Role_ID = user.Role_ID,
                    OTP = generateOTP
                };


                objEntity.Users.Add(newUser);
                objEntity.SaveChanges();

                int UserID = newUser.User_ID;
                var date = DateTime.Now;


                Password savePassword = user.Passwords.ElementAt(0);
                savePassword.Password1 = GenerateHash(savePassword.Password1);
                savePassword.Time = date.TimeOfDay;
                savePassword.Date = date.Date;
                savePassword.User_ID = UserID;


                Employee newEmployee = user.Employees.ElementAt(0);
                newEmployee.User_ID = UserID;

                objEntity.Passwords.Add(savePassword);
                objEntity.Employees.Add(newEmployee);

                objEntity.SaveChanges();
                SendOTP(newUser.Email, newUser.OTP);

                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "success";
                toReturn.UserID = UserID;

                return toReturn;
            }
            catch (Exception err)
            {
                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "error";
                toReturn.Message = err.Message;


                return toReturn;
            }


        }

        public static string GenerateHash(string inputStr)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(inputStr);
            byte[] hash = sha256.ComputeHash(bytes);

            return getStringFromHash(hash);
        }
        public static string getStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int k = 0; k < hash.Length; k++)
            {
                result.Append(hash[k].ToString("X2"));
            }
            return result.ToString();
        }
        public static void SendResetEmail(string Email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("wendalogisticsgroup@gmail.com");
                message.To.Add(new MailAddress(Email));
                message.Subject = "Password Reset";
                message.IsBodyHtml = false;
                message.Body = "Your password has been reset to:" + "\n" + "Password1*";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("wendalogisticsgroup@gmail.com", "EduSportXtra@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch
            {

            }
        }
        private void SendOTP(string Email, string OTP)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("felicityfanelo@zohomail.com");
                message.To.Add(new MailAddress(Email));
                message.Subject = "OTP";
                message.IsBodyHtml = false;
                message.Body = "Enter the below OTP to verify your email:" + "\n" + OTP;
                smtp.Port = 587;
                smtp.Host = "smtp.zoho.com";
                smtp.EnableSsl = true;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("felicityfanelo@zohomail.com", "Nghonyama.1");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch
            {

            }
        }
        public static void SendCreatedEmail(string Email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("wendalogisticsgroup@gmail.com");
                message.To.Add(new MailAddress(Email));
                message.Subject = "User Account created";
                message.IsBodyHtml = false;
                message.Body = "Your user account has been created with the following details:" + "\n" + "Username: " + Email + "\n" + "Password: Password1*";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("wendalogisticsgroup@gmail.com", "EduSportXtra@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch
            {

            }
        }

        // DELETE: api/Users/5
        
        [ResponseType(typeof(User))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await objEntity.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            objEntity.Users.Remove(user);
            objEntity.SaveChanges();
            return Ok(user);

        }

        //check user
        private bool UserExists(int id)
        {
            return objEntity.Users.Count(e => e.User_ID == id) > 0;
        }
    }
}
