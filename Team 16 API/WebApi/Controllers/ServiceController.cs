using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Service")]
    public class ServiceController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

        [HttpGet]
        [Route("AllServiceDetails")]
        public IQueryable<Service> GetService()
        {
            try
            {
                return objEntity.Services;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetServiceDetailsById/{Service_ID}")]
        public IHttpActionResult GetServiceById(string Service_ID)
        {

            Service objEmp = new Service();
            int ID = Convert.ToInt32(Service_ID);
            try
            {
                objEmp = objEntity.Services.Find(ID);
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
        [Route("InsertServiceDetails")]
        public IHttpActionResult PostService(Service data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Services.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateServiceDetails")]
        public IHttpActionResult PutService(Service service)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Service objEmp = new Service();
                objEmp = objEntity.Services.Find(service.Service_ID);
                if (objEmp != null)
                {
                    objEmp.ServiceName = service.ServiceName;
                    objEmp.ServiceDescription = service.ServiceDescription;
                    
                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(service);

        }
        [HttpDelete]
        [Route("DeleteServiceDetails")]
        public IHttpActionResult DeleteServiceDetails(int id)
        {

            Service service = objEntity.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            objEntity.Services.Remove(service);
            objEntity.SaveChanges();
            return Ok(service);

        }
    }
}
