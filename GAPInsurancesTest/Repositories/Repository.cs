using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GAPInsurancesTest.Models;

namespace GAPInsurancesTest.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        internal GAPInsurancesDBEntities db;
        internal DbSet<TEntity> dbSet;

        private bool disposed = false;

        public Repository(GAPInsurancesDBEntities db)
        {
            this.db = db;
            this.dbSet = db.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity entity = this.dbSet.Find(id);
            this.Delete(entity);

        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.db.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }
            this.dbSet.Remove(entityToDelete);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbSet.ToList();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.db.Dispose();
                }

                this.disposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await this.db.SaveChangesAsync();
        }

        public bool Exists(object id)
        {
            TEntity entity = this.dbSet.Find(id);
            return entity != null;
        }
    }
}