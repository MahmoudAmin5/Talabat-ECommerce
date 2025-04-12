using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> dbSet, ISpecification<TEntity> spec)
        {
            var query = dbSet; // dbset<Product>(). 

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria); //dbset<Product>().Where(p=>p.id==id)
            if (spec.OrderByAsc is not null)
                query = query.OrderBy(spec.OrderByAsc);
            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);
            query =
                spec.Includes.Aggregate(query, (Curr, IncludeExp) => Curr.Include(IncludeExp));
            return query;

        }
    }
}
