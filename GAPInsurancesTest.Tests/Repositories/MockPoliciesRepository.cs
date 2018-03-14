using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPInsurancesTest.Models;
using GAPInsurancesTest.Repositories;

namespace GAPInsurancesTest.Tests.Repositories
{
    class MockPoliciesRepository : IRepository<Policy>, IDisposable
    {
        private ICollection<Policy> policies = new List<Policy>();
        public MockPoliciesRepository()
        {
            Policy policy1 = new Policy
            {
                policy_id = 1,
                policy_name = "Fire Hazard",
                policy_description = "Fire hazard policy insurance",
                risk_type_id = 2,
                policy_price = 200,
                coverage_percent = 25,
                coverage_type_id = 2
            };

            this.policies.Add(policy1);
        }
        public void Delete(object id)
        {
            Policy policy = this.policies.FirstOrDefault(p => p.policy_id == (int)id);
            this.policies.Remove(policy);
        }

        public bool Exists(object id)
        {
            Policy policy = this.policies.FirstOrDefault(p => p.policy_id == (int)id);
            return policy != null;
        }

        public IEnumerable<Policy> GetAll()
        {
            return this.policies;
        }

        public async Task<Policy> GetById(object id)
        {
            Policy policy = this.policies.FirstOrDefault(p => p.policy_id == (int)id);

            Func<Policy> getPolicy = () => { return policy; };
            
            return await new Task<Policy>(getPolicy);
        }

        public void Insert(Policy entity)
        {
            this.policies.Add(entity);
        }

        public Task Save()
        {
            return null;
        }

        public void Update(Policy entity)
        {
            this.policies.Remove(this.policies.FirstOrDefault(p => p.policy_id == entity.policy_id));
            this.policies.Add(entity);
            
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MockPoliciesRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
