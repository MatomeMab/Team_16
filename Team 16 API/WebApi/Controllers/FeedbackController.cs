using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Feedback")]
    public class FeedbackController : ApiController
    {
       
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllFeedbackDetails")]
        public IQueryable<Feedback> GetFeedback()
        {
            try
            {
                return objEntity.Feedbacks;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetFeedbackDetailsById/{Feedback_ID}")]
        public IHttpActionResult GetFeedbackById(string Feedback_ID)
        {

            Feedback objEmp = new Feedback();
            int ID = Convert.ToInt32(Feedback_ID);
            try
            {
                objEmp = objEntity.Feedbacks.Find(ID);
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
        [Route("InsertFeedbackDetails")]
        public IHttpActionResult PostFeedback(Feedback data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Feedbacks.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateFeedbackDetails")]
        public IHttpActionResult PutFeedback([FromBody] Feedback feedback)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    Feedback objEmp = new Feedback();
                    // objEmp = objEntity.Feedbacks.Find(feedback.Feedback_ID);
                    if (objEmp != null)
                    {
                        objEmp.FeedbackDescription = feedback.FeedbackDescription;
                       

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteFeedbackDetails")]
        public IHttpActionResult DeleteFeedbackDetails(int id)
        {

            Feedback feedback = objEntity.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }
            objEntity.Feedbacks.Remove(feedback);
            objEntity.SaveChanges();
            return Ok(feedback);

        }
    }
}
