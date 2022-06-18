using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;


namespace WebApi.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class EmployeeController : ApiController
    {
        private EasyMovingSystemEntities5 objEntity = new EasyMovingSystemEntities5();

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
        /*
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


        }*/

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

        //new api for employee
        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            objEntity.Configuration.ProxyCreationEnabled = false;
            return objEntity.Employees.Include(zz => zz.User).AsNoTracking();
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GetEmployee(string SessionID)
        {
            User findUser = objEntity.Users.Where(zz => zz.SessionID == SessionID).FirstOrDefault();
            Employee employee = await objEntity.Employees.Where(zz => zz.User_ID == findUser.User_ID).FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("api/Employees/UpdateEmployee")]
        public dynamic UpdateEmployee([FromBody] Employee updated, string SessionID)
        {
            try
            {
                int UserID = objEntity.Users.Where(zz => zz.SessionID == SessionID).FirstOrDefault().User_ID;

                Employee updateEmployee = objEntity.Employees.Where(zz => zz.User_ID == UserID).FirstOrDefault();

                updateEmployee.EmployeeName = updated.EmployeeName;
                updateEmployee.EmployeeSurname = updated.EmployeeSurname;
                updateEmployee.PhoneNum = updated.PhoneNum;
                updateEmployee.DateOfBirth = updated.DateOfBirth;
                updateEmployee.EmergencyName = updated.EmergencyName;
                updateEmployee.EmergencySurname = updated.EmergencySurname;
                updateEmployee.EmergencyPhoneNum = updated.EmergencyPhoneNum;
                updateEmployee.User_ID = UserID;
                updateEmployee.Title_ID = updated.Title_ID;
                updateEmployee.EmployeeType_ID = updated.EmployeeType_ID;

                objEntity.SaveChanges();

                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "success";

                return toReturn;
            }
            catch (Exception err)
            {
                dynamic toReturn = new ExpandoObject();
                toReturn.Result = "error";
                toReturn.Message = err.Message;

                return toReturn;
            }
        }

        [HttpGet]
        [Route("api/Employees/ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int UserID = objEntity.Employees.Find(id).User_ID;
            User findUser = objEntity.Users.Find(UserID);

            if (findUser == null)
            {
                return BadRequest();
            }

             DateTime newDate = DateTime.Now;
            
            string pass = System.Web.Security.Membership.GeneratePassword(10, 4);

            Password newPassword = new Password();
            newPassword.Password1 = UserController.GenerateHash("Password1*");
            newPassword.Date = newDate.Date;
            newPassword.Time = newDate.TimeOfDay;
            newPassword.User_ID = findUser.User_ID;

            objEntity.Passwords.Add(newPassword);

            try
            {
                await objEntity.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            UserController.SendResetEmail(findUser.Email);
            return StatusCode(HttpStatusCode.NoContent);

        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Employee_ID)
            {
                return BadRequest();
            }

            objEntity.Entry(employee).State = EntityState.Modified;
            objEntity.Entry(employee.User).State = EntityState.Modified;

            try
            {
                await objEntity.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            User findUser = objEntity.Users.Where(zz => zz.Email == employee.User.Email).FirstOrDefault();
            Employee findEmployee = objEntity.Employees.Where(zz => zz.EmployeeName == employee.EmployeeName && zz.EmployeeSurname == employee.EmployeeSurname).FirstOrDefault();

            if (findUser != null || findEmployee != null)
            {
                return Content(HttpStatusCode.Conflict, findUser.Email);
            }

            User newUser = employee.User;
            objEntity.Users.Add(newUser);
            await objEntity.SaveChangesAsync();

            Employee newEmployee = employee;
            newEmployee.User_ID = newUser.User_ID;

            objEntity.Employees.Add(newEmployee);

            DateTime DateTimes = DateTime.Now;
            
            Password newPassword = new Password();
            newPassword.Password1 = UserController.GenerateHash("Password1*");
            newPassword.User_ID = newUser.User_ID;
            newPassword.Date = DateTimes.Date;
            newPassword.Time = DateTimes.TimeOfDay;

            objEntity.Passwords.Add(newPassword);


            await objEntity.SaveChangesAsync();
            UserController.SendCreatedEmail(newUser.Email);
            return CreatedAtRoute("DefaultApi", new { id = employee.Employee_ID }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            Employee employee = await objEntity.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            objEntity.Employees.Remove(employee);
            await objEntity.SaveChangesAsync();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objEntity.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return objEntity.Employees.Count(e => e.Employee_ID == id) > 0;
        }
    }    
}

