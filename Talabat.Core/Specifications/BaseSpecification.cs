using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } // Null
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsc { get; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }

        public BaseSpecification() { } // Get All

        public BaseSpecification(Expression<Func<T,bool>> expressionCriteria) // Get By Id
        {
            Criteria = expressionCriteria;
        }
        public void AddOrderByAsc(Expression<Func<T,Object>> OrderExpression)
        {
            OrderByAsc = OrderExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, Object>> OrderDescExpression)
        {
            OrderByDesc = OrderDescExpression;
        }

    }
}
