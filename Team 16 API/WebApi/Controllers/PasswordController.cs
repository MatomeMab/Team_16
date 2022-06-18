using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PasswordController : ApiController
    {
        private EasyMovingSystemEntities5 db = new EasyMovingSystemEntities5();
        // GET: api/Passwords
        public IQueryable<Password> GetPasswords()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Passwords;
        }

        // GET: api/Passwords/5
        [ResponseType(typeof(Password))]
        public async Task<IHttpActionResult> GetPassword(int id)
        {
            Password password = await db.Passwords.FindAsync(id);
            if (password == null)
            {
                return NotFound();
            }

            return Ok(password);
        }

        // PUT: api/Passwords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPassword(int id, Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != password.Password_ID)
            {
                return BadRequest();
            }

            db.Entry(password).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordExists(id))
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

        // POST: api/Passwords
        [ResponseType(typeof(Password))]
        public async Task<IHttpActionResult> PostPassword(Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Passwords.Add(password);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = password.Password_ID }, password);
        }

        // DELETE: api/Passwords/5
        [ResponseType(typeof(Password))]
        public async Task<IHttpActionResult> DeletePassword(int id)
        {
            Password password = await db.Passwords.FindAsync(id);
            if (password == null)
            {
                return NotFound();
            }

            db.Passwords.Remove(password);
            await db.SaveChangesAsync();

            return Ok(password);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasswordExists(int id)
        {
            return db.Passwords.Count(e => e.Password_ID == id) > 0;
        }
    }
}
