﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecsParams
    {
        private const int MaxPageSize = 10;
        private int pageSize = 5;
        public int PageSize
        {
            get {return pageSize; }
            set { pageSize = value > MaxPageSize? MaxPageSize: value; }
        }

        public int pageIndex { get; set; } = 1;
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


    }
}
