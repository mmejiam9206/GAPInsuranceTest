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
using GAPInsurancesTest.Repositories;

namespace GAPInsurancesTest.Controllers
{
    [Authorize]
    public class ClientPoliciesController : ApiController
    {
        private IRepository<ClientPolicy> clientPolicyRepository;

        public ClientPoliciesController()
        {
            this.clientPolicyRepository = new Repository<ClientPolicy>(new GAPInsurancesDBEntities());
        }

        public ClientPoliciesController(IRepository<ClientPolicy> clientPolicyRepository)
        {
            this.clientPolicyRepository = clientPolicyRepository;
        }

        // GET: api/ClientPolicies
        public IQueryable<ClientPolicy> GetClientPolicies()
        {
            return (IQueryable<ClientPolicy>)this.clientPolicyRepository.GetAll();
        }

        // GET: api/ClientPolicies/5
        [ResponseType(typeof(ClientPolicy))]
        public async Task<IHttpActionResult> GetClientPolicy(int id)
        {
            ClientPolicy clientPolicy = await this.clientPolicyRepository.GetById(id);
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

            this.clientPolicyRepository.Update(clientPolicy);

            try
            {
                await this.clientPolicyRepository.Save();
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

            this.clientPolicyRepository.Insert(clientPolicy);
            await this.clientPolicyRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = clientPolicy.client_policy_id }, clientPolicy);
        }

        // DELETE: api/ClientPolicies/5
        [ResponseType(typeof(ClientPolicy))]
        public async Task<IHttpActionResult> DeleteClientPolicy(int id)
        {
            ClientPolicy clientPolicy = await this.clientPolicyRepository.GetById(id);
            if (clientPolicy == null)
            {
                return NotFound();
            }

            this.clientPolicyRepository.Delete(clientPolicy);
            await this.clientPolicyRepository.Save();

            return Ok(clientPolicy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.clientPolicyRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientPolicyExists(int id)
        {
            return this.clientPolicyRepository.Exists(id);
        }
    }
}