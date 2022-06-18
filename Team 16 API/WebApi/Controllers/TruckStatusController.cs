using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/TruckStatus")]
    public class TruckStatusController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();
        [HttpGet]
        [Route("AllTruckStatusDetails")]
        public IQueryable<TruckStatu> GetTruckStatus()
        {
            try
            {
                return objEntity.TruckStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTruckStatusDetailsById/{TruckStatus_ID}")]
        public IHttpActionResult GetJobTruckById(string TruckStatus_ID)
        {

            TruckStatu objEmp = new TruckStatu();
            int ID = Convert.ToInt32(TruckStatus_ID);
            try
            {
                objEmp = objEntity.TruckStatus.Find(ID);
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
    }
}
