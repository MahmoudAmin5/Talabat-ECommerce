using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Core;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories = new Hashtable();
        private StoreContext _dbcontext;

        public UnitOfWork(Hashtable repositories, StoreContext dbcontext)
        {
            _repositories = repositories;
            _dbcontext = dbcontext;
        }
        public async Task<int> CompleteAsync()
        => await _dbcontext.SaveChangesAsync();

        public ValueTask DisposeAsync()
        =>  _dbcontext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var Repo = new GenericRepository<TEntity>(_dbcontext); 
                _repositories.Add(type, Repo);
            }
            return _repositories[type] as GenericRepository<TEntity>;
        }
    }
}
