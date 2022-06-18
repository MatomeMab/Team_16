using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/InspectionType")]
    public class InspectionTypeController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

        [HttpGet]
        [Route("AllInspectionTypeDetails")]
        public IQueryable<InspectionType> GetInspectionType()
        {
            try
            {
                return objEntity.InspectionTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetInspectionTypeDetailsById/{InspectionType_ID}")]
        public IHttpActionResult GetInspectionTypeById(string InspectionType_ID)
        {

            InspectionType objEmp = new InspectionType();
            int ID = Convert.ToInt32(InspectionType_ID);
            try
            {
                objEmp = objEntity.InspectionTypes.Find(ID);
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
        [Route("InsertInpectionTypeDetails")]
        public IHttpActionResult PostInspectionType(InspectionType data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.InspectionTypes.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateInspectionTypeDetails")]
        public IHttpActionResult PutInspectionType(InspectionType inType)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                InspectionType objEmp = new InspectionType();
                objEmp = objEntity.InspectionTypes.Find(inType.InspectionType_ID);
                if (objEmp != null)
                {
                    objEmp.TypeName = inType.TypeName;
                    objEmp.TypeDescription = inType.TypeDescription;

                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(inType);

        }
        [HttpDelete]
        [Route("DeleteInspectionTypeDetails")]
        public IHttpActionResult DeleteInspectionTypeDetails(int id)
        {

           InspectionType inType = objEntity.InspectionTypes.Find(id);
            if (inType == null)
            {
                return NotFound();
            }
            objEntity.InspectionTypes.Remove(inType);
            objEntity.SaveChanges();
            return Ok(inType);

        }

    }
}
