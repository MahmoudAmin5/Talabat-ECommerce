using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecsParams
    {
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }

    }
}
