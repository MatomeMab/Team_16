using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Client")]
    public class ClientController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

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


        }

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
    }
}
