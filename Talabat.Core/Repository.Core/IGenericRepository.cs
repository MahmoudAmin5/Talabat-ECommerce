using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Core
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T?> GetAsync(int id);
       public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T?> GetWithSpecificationAsync(ISpecification<T> spec);
        public Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> spec);
        public Task<int> GetCountWithSpecificationAsync(ISpecification<T> spec);
    }
}
