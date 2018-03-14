using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GAPInsurancesTest.Models;

namespace GAPInsurancesTest.Controllers
{
    [Authorize]
    public class ClientPoliciesController : ApiController
    {
        private GAPInsurancesDBEntities db = new GAPInsurancesDBEntities();

        // GET: api/ClientPolicies
        public IQueryable<ClientPolicy> GetClientPolicies()
        {
            return db.ClientPolicies;
        }

        // GET: api/ClientPolicies/5
        [ResponseType(typeof(ClientPolicy))]
        public async Task<IHttpActionResult> GetClientPolicy(int id)
        {
            ClientPolicy clientPolicy = await db.ClientPolicies.FindAsync(id);
            if (clientPolicy == null)
            {
                return NotFound();
            }

            return Ok(clientPolicy);
        }

        // PUT: api/ClientPolicies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClientPolicy(int id, ClientPolicy clientPolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientPolicy.client_policy_id)
            {
                return BadRequest();
            }

            db.Entry(clientPolicy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientPolicyExists(id))
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

        // POST: api/ClientPolicies
        [ResponseType(typeof(ClientPolicy))]
        public async Task<IHttpActionResult> PostClientPolicy(ClientPolicy clientPolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientPolicies.Add(clientPolicy);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = clientPolicy.client_policy_id }, clientPolicy);
        }

        // DELETE: api/ClientPolicies/5
        [ResponseType(typeof(ClientPolicy))]
        public async Task<IHttpActionResult> DeleteClientPolicy(int id)
        {
            ClientPolicy clientPolicy = await db.ClientPolicies.FindAsync(id);
            if (clientPolicy == null)
            {
                return NotFound();
            }

            db.ClientPolicies.Remove(clientPolicy);
            await db.SaveChangesAsync();

            return Ok(clientPolicy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientPolicyExists(int id)
        {
            return db.ClientPolicies.Count(e => e.client_policy_id == id) > 0;
        }
    }
}