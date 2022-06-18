using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Client")]
    public class ClientController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

        //Old links for client
        [HttpGet]
        [Route("AllClientDetails")]
        public IQueryable<Client> GetClient()
        {
            try
            {
                return objEntity.Clients;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetClientDetailsById/{Client_ID}")]
        public IHttpActionResult GetClientById(string Client_ID)
        {

            Client objEmp = new Client();
            int ID = Convert.ToInt32(Client_ID);
            try
            {
                objEmp = objEntity.Clients.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(objEmp);

        }
        /*
        [HttpPost]
        [Route("InsertClientDetails")]
        public IHttpActionResult PostClient(Client data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Clients.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }*/

        [HttpPut]
        [Route("UpdateClientDetails")]
        public IHttpActionResult PutClient([FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    Client objEmp = new Client();
                    // objEmp = objEntity.Clients.Find(client.Client_ID);
                    if (objEmp != null)
                    {
                        objEmp.ClientName = client.ClientName;
                        objEmp.ClientSurname = client.ClientSurname;
                        objEmp.PhoneNum = client.PhoneNum;

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteClientDetails")]
        public IHttpActionResult DeleteClientDetails(int id)
        {

            Client client = objEntity.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            objEntity.Clients.Remove(client);
            objEntity.SaveChanges();
            return Ok(client);

        }

        //new links for the client
        // GET: api/Students
        public List<dynamic> GetClients()
        {
            objEntity.Configuration.ProxyCreationEnabled = false;
            return formatClients(objEntity.Clients.ToList());
        }

        private List<dynamic> formatClients(List<Client> clientlist)
        {
            List<dynamic> list = new List<dynamic>();

            foreach (Client client in clientlist)
            {
                dynamic getClient = new ExpandoObject();

                getClient.Client_ID = client.Client_ID;
                getClient.ClientName = client.ClientName;
                getClient.ClientSurname = client.ClientSurname;
                getClient.PhoneNum = client.PhoneNum;
                getClient.User_ID = client.User_ID;

                User getUser = objEntity.Users.Find(client.User_ID);

                getClient.Status = getUser.Active.ToString();
                getClient.Email = getUser.Email;

               

                list.Add(getClient);
            }


            return list;

        }


        [HttpPut]
        [Route("api/Clients/UpdateClient")]
        public dynamic UpdateClient([FromBody] Client updated, string SessionID)
        {
            try
            {
                int UserID = objEntity.Users.Where(zz => zz.SessionID == SessionID).FirstOrDefault().User_ID;

                Client updateClient = objEntity.Clients.Where(zz => zz.User_ID == UserID).FirstOrDefault();

                updateClient.ClientName = updated.ClientName;
                updateClient.ClientSurname = updated.ClientSurname;
                updateClient.PhoneNum = updated.PhoneNum;
                

                objEntity.SaveChanges();

                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "success";

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

        [HttpGet]
        [Route("api/Clients/GetClient")]
        public dynamic GetClient(string id)
        {
            try
            {
                int finduserid = objEntity.Users.Where(zz => zz.SessionID == id).FirstOrDefault().User_ID;
                Client getClient = objEntity.Clients.Where(zz => zz.User_ID == finduserid).FirstOrDefault();

                return formatClient(getClient);
            }
            catch
            {
                return BadRequest();
            }
        }

        private dynamic formatClient(Client client)
        {
            dynamic getClient = new ExpandoObject();

            getClient.Client_ID = client.Client_ID;
            getClient.ClientName = client.ClientName;
            getClient.ClientSurname = client.ClientSurname;
            getClient.PhoneNum = client.PhoneNum;
            getClient.User_ID = client.User_ID;
           

            return getClient;
        }


        [HttpGet]
        [Route("api/Clients/ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int UserID = objEntity.Clients.Find(id).User_ID;
            User findUser = objEntity.Users.Find(UserID);

            if (findUser == null)
            {
                return BadRequest();
            }

            DateTime newDate = DateTime.Now;

            Password newPassword = new Password();
            newPassword.Password1 = UserController.GenerateHash("Password1*");
            newPassword.Date = newDate.Date;
            newPassword.Time = newDate.TimeOfDay;
            newPassword.User_ID = findUser.User_ID;

            objEntity.Passwords.Add(newPassword);

            try
            {
                await objEntity.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            UserController.SendResetEmail(findUser.Email);
            return StatusCode(HttpStatusCode.NoContent);

        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Client_ID)
            {
                return BadRequest();
            }

            objEntity.Entry(client).State = EntityState.Modified;
            objEntity.Entry(client.User).State = EntityState.Modified;


            try
            {
                await objEntity.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            User findUser = objEntity.Users.Where(zz => zz.Email == client.User.Email).FirstOrDefault();
            Client findStudent = objEntity.Clients.Where(zz => zz.ClientName == client.ClientName && zz.ClientSurname == client.ClientSurname).FirstOrDefault();

            if (findUser != null || findStudent != null)
            {
                return Content(HttpStatusCode.Conflict, findUser.Email);
            }

            User newUser = client.User;
            objEntity.Users.Add(newUser);
            await objEntity.SaveChangesAsync();

            Client newClient= client;
            newClient.User_ID = newUser.User_ID;

            objEntity.Clients.Add(newClient);

            DateTime DateTimes = DateTime.Now;

            Password newPassword = new Password();
            newPassword.Password1 = UserController.GenerateHash("Password1*");
            newPassword.User_ID = newUser.User_ID;
            newPassword.Date = DateTimes.Date;
            newPassword.Time = DateTimes.TimeOfDay;

            objEntity.Passwords.Add(newPassword);

            await objEntity.SaveChangesAsync();
            UserController.SendCreatedEmail(newUser.Email);
            return CreatedAtRoute("DefaultApi", new { id = client.Client_ID }, client);
        }

        // DELETE: api/Client/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(int id)
        {
            Client student = await objEntity.Clients.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            objEntity.Clients.Remove(student);
            await objEntity.SaveChangesAsync();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objEntity.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return objEntity.Clients.Count(e => e.Client_ID == id) > 0;
        }
    }
}
