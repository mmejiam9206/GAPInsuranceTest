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
    public class PoliciesController : ApiController
    {
        private GAPInsurancesDBEntities db = new GAPInsurancesDBEntities();

        // GET: api/Policies
        public IQueryable<Policy> GetPolicies()
        {
            return db.Policies;
        }

        // GET: api/Policies/5
        [ResponseType(typeof(Policy))]
        public async Task<IHttpActionResult> GetPolicy(int id)
        {
            Policy policy = await db.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }

        // PUT: api/Policies/5
        [ResponseType(typeof(void))]
        [Authorize]
        public async Task<IHttpActionResult> PutPolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.policy_id)
            {
                return BadRequest();
            }

                        // If risk type is high and the coverage is over 50%, throw an error
            if (policy.RiskType.risk_type_id == 4 && policy.coverage_percent > 50)
            {
                return BadRequest("The coverage of a high risk policy cannot be greater than 50%");
            }

            db.Entry(policy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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

        // POST: api/Policies
        [ResponseType(typeof(Policy))]
        [Authorize]
        public async Task<IHttpActionResult> PostPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // If risk type is high and the coverage is over 50%, throw an error
                if (policy.risk_type_id == 4 && policy.coverage_percent > 50)
                {
                    throw new Exception("The coverage of a high risk policy cannot be greater than 50%");
                }

                if (policy.policy_price <= 0 || policy.coverage_percent <= 0)
                {
                    throw new Exception("Policy price and coverage percent must be greater than 0");
                }

                db.Policies.Add(policy);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = policy.policy_id }, policy);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Policies/5
        [ResponseType(typeof(Policy))]
        [Authorize]
        public async Task<IHttpActionResult> DeletePolicy(int id)
        {
            Policy policy = await db.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            db.Policies.Remove(policy);
            await db.SaveChangesAsync();

            return Ok(policy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PolicyExists(int id)
        {
            return db.Policies.Count(e => e.policy_id == id) > 0;
        }
    }
}