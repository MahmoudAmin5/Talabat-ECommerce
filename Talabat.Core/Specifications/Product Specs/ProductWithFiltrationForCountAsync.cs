using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithFiltrationForCountAsync:BaseSpecification<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecsParams specsParams)
            : base(p =>
                   (!specsParams.brandId.HasValue || p.BrandId == specsParams.brandId) &&
                   (!specsParams.categoryId.HasValue || p.CategoryId == specsParams.categoryId)
                 )
        {
            
        }
    }
}
