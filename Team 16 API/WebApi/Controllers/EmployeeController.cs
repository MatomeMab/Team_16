using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;


namespace WebApi.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class EmployeeController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IQueryable<Employee> GetEmployee()
        {
            try
            {
                return objEntity.Employees;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{Employee_ID}")]
        public IHttpActionResult GetEmployeeById(string Employee_ID)
        {

            Employee objEmp = new Employee();
            int ID = Convert.ToInt32(Employee_ID);
            try
            {
                objEmp = objEntity.Employees.Find(ID);
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
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmployee(Employee data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Employees.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmployee([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    Employee objEmp = new Employee();
                    objEmp = objEntity.Employees.Find(employee.Employee_ID);
                    if (objEmp != null)
                    {
                        objEmp.EmployeeName = employee.EmployeeName;
                        objEmp.EmployeeSurname = employee.EmployeeSurname;
                        objEmp.PhoneNum = objEmp.PhoneNum;
                        objEmp.DateOfBirth = objEmp.DateOfBirth;
                        objEmp.DateEmployed = objEmp.DateEmployed;
                        objEmp.EmergencyName = objEmp.EmergencyName;
                        objEmp.EmergencySurname = objEmp.EmergencySurname;
                        objEmp.EmergencyPhoneNum = objEmp.EmergencyPhoneNum;

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmployeeDetails(int id)
        {

            Employee employees = objEntity.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }
            objEntity.Employees.Remove(employees);
            objEntity.SaveChanges();
            return Ok(employees);

        }

    }    
}

