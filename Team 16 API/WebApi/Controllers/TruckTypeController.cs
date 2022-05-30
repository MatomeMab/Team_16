using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    [RoutePrefix("Api/TruckType")]
    public class TruckTypeController : ApiController
    {
        private EasyMovingSystemEntities3 objEntity = new EasyMovingSystemEntities3();
        [HttpGet]
        [Route("AllTruckTypeDetails")]
        public IQueryable<TruckType> GetTruckType()
        {
            try
            {
                return objEntity.TruckTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTruckTypeDetailsById/{TruckType_ID}")]
        public IHttpActionResult GetTruckTypeById(string TruckType_ID)
        {

            TruckType objEmp = new TruckType();
            int ID = Convert.ToInt32(TruckType_ID);
            try
            {
                objEmp = objEntity.TruckTypes.Find(ID);
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
        [Route("InsertTruckTypeDetails")]
        public IHttpActionResult PostTruckType(TruckType data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.TruckTypes.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateTruckTypeDetails")]
        public IHttpActionResult PutTruckType([FromBody] TruckType truckType)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    TruckType objEmp = new TruckType();
                    objEmp = objEntity.TruckTypes.Find(truckType.TruckType_ID);
                    if (objEmp != null)
                    {
                        objEmp.TruckTypeName = truckType.TruckTypeName;
                        objEmp.TruckTypeDescription = truckType.TruckTypeDescription;

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(truckType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteTruckTypeDetails")]
        public IHttpActionResult DeleteTruckTypeDetails(int id)
        {

            TruckType truckType = objEntity.TruckTypes.Find(id);
            if (truckType == null)
            {
                return NotFound();
            }
            objEntity.TruckTypes.Remove(truckType);
            objEntity.SaveChanges();
            return Ok(truckType);

        }
    }
}
