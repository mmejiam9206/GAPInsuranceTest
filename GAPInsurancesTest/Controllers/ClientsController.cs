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
    public class ClientsController : ApiController
    {
        private IRepository<Client> clientRepository;

        public ClientsController()
        {
            this.clientRepository = new Repository<Client>(new GAPInsurancesDBEntities());
        }

        public ClientsController(IRepository<Client> clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        // GET: api/Clients
        [AllowAnonymous]
        public IQueryable<Client> GetClients()
        {
            return (IQueryable<Client>)this.clientRepository.GetAll();
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(int id)
        {
            Client client = await this.clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.client_id)
            {
                return BadRequest();
            }

            this.clientRepository.Update(client);

            try
            {
                await this.clientRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.clientRepository.Insert(client);
            await this.clientRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = client.client_id }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(int id)
        {
            Client client = await this.clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            this.clientRepository.Delete(client);
            await this.clientRepository.Save();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.clientRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return this.clientRepository.Exists(id);
        }
    }
}