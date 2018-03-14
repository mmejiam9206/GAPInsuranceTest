using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAPInsurancesTest.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(object id);
        bool Exists(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        Task Save();
    }
}
