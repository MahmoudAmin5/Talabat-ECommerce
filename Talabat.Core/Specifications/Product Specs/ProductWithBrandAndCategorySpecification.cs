using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Product_Specs
{
    public class ProductWithBrandAndCategorySpecification:BaseSpecification<Product>
    {
        public ProductWithBrandAndCategorySpecification(ProductSpecsParams specsParams)
            :base(p=>
                   (!specsParams.brandId.HasValue || p.BrandId == specsParams.brandId)&&
                   (!specsParams.categoryId.HasValue || p.CategoryId==specsParams.categoryId)
                 )
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(specsParams.sort))
            {
                switch(specsParams.sort)
                {
                    case "priceAsc":
                        AddOrderByAsc(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    default:
                        AddOrderByAsc(p => p.Name);
                        break;
                }
            }
            else 
                AddOrderByAsc(p=>p.Name);

        }
        public ProductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
