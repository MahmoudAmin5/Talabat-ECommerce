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
        public async Task<IReadOnlyList<T>> GetAllAsync()
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
        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> spec)
        {
            return
                await ApplySpecifications(spec).AsNoTracking().ToListAsync();

        }
        public async Task<int> GetCountWithSpecificationAsync(ISpecification<T> spec)
        {
            return 
                await ApplySpecifications(spec).CountAsync();
        }
        #endregion
        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return 
                SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }

        public async Task AddAsync(T AddedItem)
        => await _dbcontext.Set<T>().AddAsync(AddedItem);

        public async Task UpdateAsync(T UpdatedItem)
        =>  _dbcontext.Set<T>().Update(UpdatedItem);

        public void DeleteAsync(T DeletedItem)
        => _dbcontext.Set<T>().Remove(DeletedItem);
    }
}
