using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/JobListing")]
    public class JobListingController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllJobListingDetails")]
        public IQueryable<JobListing> GetJobListing()
        {
            try
            {
                return objEntity.JobListings;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetJobListingDetailsById/{JobListing_ID}")]
        public IHttpActionResult GetJobListingById(string JobListing_ID)
        {

            JobListing objEmp = new JobListing();
            int ID = Convert.ToInt32(JobListing_ID);
            try
            {
                objEmp = objEntity.JobListings.Find(ID);
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
        [Route("InsertJobListingDetails")]
        public IHttpActionResult PostJobListing(JobListing data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.JobListings.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateJobListingDetails")]
        public IHttpActionResult PutJobListing(JobListing jobListing)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                JobListing objEmp = new JobListing();
                objEmp = objEntity.JobListings.Find(jobListing.Job_ID);
                if (objEmp != null)
                {
                    objEmp.Description = jobListing.Description;
                    objEmp.Amount = jobListing.Amount;
                    objEmp.ListingStatus_ID = objEmp.ListingStatus_ID;
                    objEmp.DatePosted = objEmp.DatePosted;
                    objEmp.ExpiryDate = jobListing.ExpiryDate;
                  
                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(jobListing);

        }
        [HttpDelete]
        [Route("DeleteJobListingDetails")]
        public IHttpActionResult DeleteJobListingDetails(int id)
        {

            JobListing jobListing = objEntity.JobListings.Find(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            objEntity.JobListings.Remove(jobListing);
            objEntity.SaveChanges();
            return Ok(jobListing);

        }
    }
}
