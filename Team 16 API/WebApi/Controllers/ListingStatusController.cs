using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/ListingStatus")]
    public class ListingStatusController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();
        [HttpGet]
        [Route("AllListingStatusDetails")]
        public IQueryable<ListingStatu> GetListingStatus()
        {
            try
            {
                return objEntity.ListingStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetListingStatusDetailsById/{ListingStatus_ID}")]
        public IHttpActionResult GetJobListingById(string ListingStatus_ID)
        {

            ListingStatu objEmp = new ListingStatu();
            int ID = Convert.ToInt32(ListingStatus_ID);
            try
            {
                objEmp = objEntity.ListingStatus.Find(ID);
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
