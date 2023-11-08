using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.DTOs;
using Talabat.API.Error;
using Talabat.CoreEntities.Entites;
using Talabat.CoreEntities.Repositotry;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductController(

            IGenericRepository<Product> productRepo,
            IMapper mapper,
            IGenericRepository<ProductType> productTypeRepo)

        {
            _productRepo = productRepo;
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var allProducts = await _productRepo.GetAllAsync().Result.Include(a => a.Brand).Include(a => a.ProductType).ToListAsync();
            if (allProducts is null)
                return NotFound(new ApiResponse(404, "NO products were Found"));
            return Ok(allProducts);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
                return NotFound(new ApiResponse(404, "the product Not found"));
            return Ok(product);

        }

        [HttpGet("GetProductsDetailsById/{id}")]
        public IActionResult GetProductDetails(int id)
        {
            var product = _productRepo.GetAll(a => a.Id == id)
                .Include(a => a.Brand)
                .Include(a => a.ProductType)
                .FirstOrDefault();
            if (product is null)
                return BadRequest(new ApiResponse(404, null));
            var mappedProduct = _mapper.Map<ProductDTO>(product);
            return Ok(mappedProduct);
        }
        [HttpGet("ProductTypes")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            var allTypes = await _productTypeRepo.GetAllAsync().Result.ToListAsync();
            if (allTypes.Count == 0)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "No Types Were Found"));

            return Ok(allTypes);
        }

    }
}
