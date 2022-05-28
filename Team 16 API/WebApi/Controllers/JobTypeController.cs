using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    [RoutePrefix("Api/JobType")]
    public class JobTypeController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();
        [HttpGet]
        [Route("AllJobTypeDetails")]
        public IQueryable<JobType> GetJobType()
        {
            try
            {
                return objEntity.JobTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetJobTypeDetailsById/{JobType_ID}")]
        public IHttpActionResult GetJobTypeById(string JobType_ID)
        {

            JobType objEmp = new JobType();
            int ID = Convert.ToInt32(JobType_ID);
            try
            {
                objEmp = objEntity.JobTypes.Find(ID);
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
