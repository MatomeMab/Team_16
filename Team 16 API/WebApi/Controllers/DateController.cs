using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;


namespace WebApi.Controllers
{
    [RoutePrefix("Api/Date")]
    public class DateController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

        [HttpGet]
        [Route("AllDatesDetails")]
        public IQueryable<Date> GetDate()
        {
            try
            {
                return objEntity.Dates;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetDateDetailsById/{Date_ID}")]
        public IHttpActionResult GetDateById(string Date_ID)
        {

            Date objEmp = new Date();
            int ID = Convert.ToInt32(Date_ID);
            try
            {
                objEmp = objEntity.Dates.Find(ID);
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
        [Route("InsertDateDetails")]
        public IHttpActionResult PostDate(Date data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Dates.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateDateDetails")]
        public IHttpActionResult PutDate(Date date)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Date objEmp = new Date();
                objEmp = objEntity.Dates.Find(date.Date_ID);
                if (objEmp != null)
                {
                    objEmp.Date1 = date.Date1;
                   
                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(date);

        }
        [HttpDelete]
        [Route("DeleteDateDetails")]
        public IHttpActionResult DeleteDateDetails(int id)
        {

            Date date = objEntity.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }
            objEntity.Dates.Remove(date);
            objEntity.SaveChanges();
            return Ok(date);

        }
    }
}
