using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/EmployeeStatus")]
    public class EmployeeStatusController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();
        [HttpGet]
        [Route("AllEmployeeStatusDetails")]
        public IQueryable<EmployeeStatu> GetEmployeeStatus()
        {
            try
            {
                return objEntity.EmployeeStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeStatusDetailsById/{EmployeeStatus_ID}")]
        public IHttpActionResult GetJobEmployeeById(string EmployeeStatus_ID)
        {

            EmployeeStatu objEmp = new EmployeeStatu();
            int ID = Convert.ToInt32(EmployeeStatus_ID);
            try
            {
                objEmp = objEntity.EmployeeStatus.Find(ID);
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
