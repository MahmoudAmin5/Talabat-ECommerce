using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Product_Specs
{
    public class ProductWithBrandAndCategorySpecification:BaseSpecification<Product>
    {
        public ProductWithBrandAndCategorySpecification()
            :base()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
        public ProductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
