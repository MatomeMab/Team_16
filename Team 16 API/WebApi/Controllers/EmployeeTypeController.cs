using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/EmployeeType")]
    public class EmployeeTypeController : ApiController
    {
    
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllEmployeeTypeDetails")]
        public IQueryable<EmployeeType> GetEmployeeType()
        {
            try
            {
                return objEntity.EmployeeTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeTypeDetailsById/{EmployeeType_ID}")]
        public IHttpActionResult GetEmployeeTypeById(string EmployeeType_ID)
        {

            EmployeeType objEmp = new EmployeeType();
            int ID = Convert.ToInt32(EmployeeType_ID);
            try
            {
                objEmp = objEntity.EmployeeTypes.Find(ID);
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
        [Route("InsertEmployeeTypeDetails")]
        public IHttpActionResult PostEmployeeType(EmployeeType data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.EmployeeTypes.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateEmployeeTypeDetails")]
        public IHttpActionResult PutEmployeeType([FromBody]EmployeeType employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    EmployeeType objEmp = new EmployeeType();
                   // objEmp = objEntity.EmployeeTypes.Find(employee.EmployeeType_ID);
                    if (objEmp != null)
                    {
                        objEmp.EmployeeTypeName = employee.EmployeeTypeName;
                        objEmp.EmployeeDescription = employee.EmployeeDescription;

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(employee);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeTypeDetails")]
        public IHttpActionResult DeleteEmployeeTypeDetails(int id)
        {

            EmployeeType employee = objEntity.EmployeeTypes.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            objEntity.EmployeeTypes.Remove(employee);
            objEntity.SaveChanges();
            return Ok(employee);

        }
    }
}
