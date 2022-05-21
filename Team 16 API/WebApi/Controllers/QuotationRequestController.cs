using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/QuotationRequest")]
    public class QuotationRequestController : ApiController
    {
        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllQuotationRequestDetails")]
        public IQueryable<QuotationRequest> GetQuotationRequest()
        {
            try
            {
                return objEntity.QuotationRequests;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetQuotationRequestDetailsById/{QuotationRequest_ID}")]
        public IHttpActionResult GetQuotationRequestById(string QuotationRequest_ID)
        {

            QuotationRequest objEmp = new QuotationRequest();
            int ID = Convert.ToInt32(QuotationRequest_ID);
            try
            {
                objEmp = objEntity.QuotationRequests.Find(ID);
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
        [Route("InsertQuotationRequestDetails")]
        public IHttpActionResult PostQuotationRequest(QuotationRequest data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.QuotationRequests.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateQuotationRequestDetails")]
        public IHttpActionResult PutQuotationRequest(QuotationRequest quotationRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                QuotationRequest objEmp = new QuotationRequest();
                objEmp = objEntity.QuotationRequests.Find(quotationRequest.QuotationRequest_ID);
                if (objEmp != null)
                {
                    objEmp.FromAddress = quotationRequest.FromAddress;
                    objEmp.ToAddress = quotationRequest.ToAddress;
                    
                }
                this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(quotationRequest);

        }
        [HttpDelete]
        [Route("DeleteQuotationRequestDetails")]
        public IHttpActionResult DeleteQuotationRequestDetails(int id)
        {

            QuotationRequest quotationRequest = objEntity.QuotationRequests.Find(id);
            if (quotationRequest == null)
            {
                return NotFound();
            }
            objEntity.QuotationRequests.Remove(quotationRequest);
            objEntity.SaveChanges();
            return Ok(quotationRequest);

        }
    }
}
