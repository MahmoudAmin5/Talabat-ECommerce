using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Core;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Wihout Specifications 
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _dbcontext.Set<T>().FirstAsync(p => p.Id == id);
        }
        #endregion

        #region With Specifications
        public async Task<T?> GetWithSpecificationAsync(ISpecification<T> spec)
        {

            return
               await ApplySpecifications(spec).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> spec)
        {
            return
                await ApplySpecifications(spec).AsNoTracking().ToListAsync();

        } 
        #endregion
        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return 
                SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }
    }
}
