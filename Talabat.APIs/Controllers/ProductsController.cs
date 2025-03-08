using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Repository.Core;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecification();
            var products = await _productRepository.GetAllWithSpecificationAsync(spec);

            return Ok (_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecification(id);
            var product = await _productRepository.GetWithSpecificationAsync(spec);
            if (product is null)
                return NotFound();
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
    }
}
