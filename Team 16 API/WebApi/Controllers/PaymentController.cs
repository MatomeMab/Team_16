using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Payment")]
    public class PaymentController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllPaymentDetails")]
        public IQueryable<Payment> GetPayment()
        {
            try
            {
                return objEntity.Payments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetPaymentDetailsById/{Payment_ID}")]
        public IHttpActionResult GetPaymentById(string Payment_ID)
        {

            Payment objEmp = new Payment();
            int ID = Convert.ToInt32(Payment_ID);
            try
            {
                objEmp = objEntity.Payments.Find(ID);
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
        [Route("InsertPaymentDetails")]
        public IHttpActionResult PostPayment(Payment data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Payments.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdatePaymentDetails")]
        public IHttpActionResult PutPayment([FromBody] Payment payment)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    Payment objEmp = new Payment();
                    // objEmp = objEntity.Payments.Find(payment.Payment_ID);
                    if (objEmp != null)
                    {
                        objEmp.AmountPaid = payment.AmountPaid;
                        objEmp.AmountDue = payment.AmountDue;


                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeletePaymentDetails")]
        public IHttpActionResult DeletePaymentDetails(int id)
        {

            Payment payment = objEntity.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            objEntity.Payments.Remove(payment);
            objEntity.SaveChanges();
            return Ok(payment);

        }
    }
}
