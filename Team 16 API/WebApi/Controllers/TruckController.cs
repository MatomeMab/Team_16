using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    [RoutePrefix("Api/Truck")]
    public class TruckController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

        [HttpGet]
        [Route("AllTruckDetails")]
        public IQueryable<Truck> GetTruck()
        {
            try
            {
                return objEntity.Trucks;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTruckDetailsById/{Truck_ID}")]
        public IHttpActionResult GetTruckById(string Truck_ID)
        {

            Truck objEmp = new Truck();
            int ID = Convert.ToInt32(Truck_ID);
            try
            {
                objEmp = objEntity.Trucks.Find(ID);
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
        [Route("InsertTruckDetails")]
        public IHttpActionResult PostTruck(Truck data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Trucks.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateTruckDetails")]
        public IHttpActionResult PutTruck(Truck truck)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Truck objEmp = new Truck();
                objEmp = objEntity.Trucks.Find(truck.Truck_ID);
                if (objEmp != null)
                {
                    objEmp.Model = truck.Model;
                    objEmp.Make = truck.Make;
                    objEmp.RegNum = objEmp.RegNum;
                    objEmp.Year = truck.Year;
                    objEmp.Colour = truck.Colour;
                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(truck);

        }
        [HttpDelete]
        [Route("DeleteTruckDetails")]
        public IHttpActionResult DeleteTruckDetails(int id)
        {

            Truck truck = objEntity.Trucks.Find(id);
            if (truck == null)
            {
                return NotFound();
            }
            objEntity.Trucks.Remove(truck);
            objEntity.SaveChanges();
            return Ok(truck);

        }
    }
}
