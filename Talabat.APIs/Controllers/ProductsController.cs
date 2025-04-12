using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Repository.Core;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;

        public ProductsController(
            IGenericRepository<Product> productRepository,IMapper mapper
            , IGenericRepository<ProductBrand> brandRepo, 
            IGenericRepository<ProductCategory> categoryRepo
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecsParams specsParams )
        {
            var spec = new ProductWithBrandAndCategorySpecification(specsParams);
            var products = await _productRepository.GetAllWithSpecificationAsync(spec);

            return Ok (_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecification(id);
            var product = await _productRepository.GetWithSpecificationAsync(spec);
            if (product is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("category")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategory()
        { 
                var categories = await _categoryRepo.GetAllAsync();
                return Ok(categories);
        }
    }
}
