using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Time")]
    public class TimeController : ApiController
    {
        private EasyMovingSystemEntities3 objEntity = new EasyMovingSystemEntities3();

        [HttpGet]
        [Route("AllTimeSlotDetails")]
        public IQueryable<TimeSlot> GetTimeSlot()
        {
            try
            {
                return objEntity.TimeSlots;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTimeSlotDetailsById/{TimeSlot_ID}")]
        public IHttpActionResult GetTimeSlotById(string TimeSlot_ID)
        {

            TimeSlot objEmp = new TimeSlot();
            int ID = Convert.ToInt32(TimeSlot_ID);
            try
            {
                objEmp = objEntity.TimeSlots.Find(ID);
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
        [Route("InsertTimeSlotDetails")]
        public IHttpActionResult PostTimeSlot(TimeSlot data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.TimeSlots.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateTimeSlotDetails")]
        public IHttpActionResult PutTimeSlot(TimeSlot timeSlot)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                TimeSlot objEmp = new TimeSlot();
                objEmp = objEntity.TimeSlots.Find(timeSlot.TimeSlot_ID);
                if (objEmp != null)
                {
                    objEmp.StartTime = timeSlot.StartTime;
                    objEmp.EndTime = objEmp.EndTime;
                   
                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(timeSlot);

        }
        [HttpDelete]
        [Route("DeleteTimeSlotDetails")]
        public IHttpActionResult DeleteTimeSlotDetails(int id)
        {

            TimeSlot timeSlot = objEntity.TimeSlots.Find(id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            objEntity.TimeSlots.Remove(timeSlot);
            objEntity.SaveChanges();
            return Ok(timeSlot);

        }
    }
}
