using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Booking")]
    public class BookingController : ApiController
    {

        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllBookingDetails")]
        public IQueryable<Booking> GetBooking()
        {
            try
            {
                return objEntity.Bookings;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetBookingDetailsById/{Booking_ID}")]
        public IHttpActionResult GetBookingById(string Booking_ID)
        {

            Booking objEmp = new Booking();
            int ID = Convert.ToInt32(Booking_ID);
            try
            {
                objEmp = objEntity.Bookings.Find(ID);
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
        [Route("InsertBookingDetails")]
        public IHttpActionResult PostBooking(Booking data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Bookings.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateBookingDetails")]
        public IHttpActionResult PutBooking([FromBody] Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    Booking objEmp = new Booking();
                    // objEmp = objEntity.Bookings.Find(booking.Booking_ID);
                    if (objEmp != null)
                    {
                        objEmp.DateMade = objEmp.DateMade;
                        

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteBookingDetails")]
        public IHttpActionResult DeleteBookingDetails(int id)
        {

            Booking booking = objEntity.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            objEntity.Bookings.Remove(booking);
            objEntity.SaveChanges();
            return Ok(booking);

        }
    }
}
