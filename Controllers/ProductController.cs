using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;

        public ProductController(

            IGenericRepository<Product> productRepo, IMapper mapper

            )
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetProducts()
        {
            var allProducts = await _productRepo.GetAllAsync().Result.Include(a => a.Brand).Include(a => a.ProductType).ToListAsync();
            if (allProducts is null)
                return NotFound(new ApiResponse(400, null));
            return Ok(allProducts);

        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
                return NotFound(new ApiResponse(400, null));
            return Ok(product);

        }

        [HttpGet("GetProductsDetailsById/{id}")]
        public IActionResult GetProductDetails(int id)
        {
            var product = _productRepo.GetAll(a => a.Id == id)
                .Include(a => a.Brand)
                .Include(a => a.ProductType)
                .FirstOrDefault();
            var mappedProduct = _mapper.Map<ProductDTO>(product);
            return Ok(mappedProduct);
        }

    }
}
