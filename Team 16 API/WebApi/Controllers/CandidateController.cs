using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Candidate")]
    public class CandidateController : ApiController
    {

        private EasyMovingSystemEntities1 objEntity = new EasyMovingSystemEntities1();

        [HttpGet]
        [Route("AllCandidateDetails")]
        public IQueryable<Candidate> GetCandidate()
        {
            try
            {
                return objEntity.Candidates;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetCandidateDetailsById/{Candidate_ID}")]
        public IHttpActionResult GetCandidateById(string Candidate_ID)
        {

            Candidate objEmp = new Candidate();
            int ID = Convert.ToInt32(Candidate_ID);
            try
            {
                objEmp = objEntity.Candidates.Find(ID);
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
        [Route("InsertCandidateDetails")]
        public IHttpActionResult PostCandidate(Candidate data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                objEntity.Candidates.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);


        }

        [HttpPut]
        [Route("UpdateCandidateDetails")]
        public IHttpActionResult PutCandidate([FromBody] Candidate candidate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    Candidate objEmp = new Candidate();
                    // objEmp = objEntity.Candidates.Find(candidate.Candidate_ID);
                    if (objEmp != null)
                    {
                        objEmp.CandidateName = candidate.CandidateName;
                        objEmp.CandidateSurname = candidate.CandidateSurname;
                        objEmp.CandidatePhonNum = candidate.CandidatePhonNum;
                        objEmp.CandidateEmailAddress = candidate.CandidateEmailAddress;

                    }
                    this.objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteCandidateDetails")]
        public IHttpActionResult DeleteCandidateDetails(int id)
        {

            Candidate candidate = objEntity.Candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }
            objEntity.Candidates.Remove(candidate);
            objEntity.SaveChanges();
            return Ok(candidate);

        }
    }
}
