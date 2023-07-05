using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.CoreEntities.Entites;
using Talabat.CoreEntities.Repositotry;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(

            IGenericRepository<Product> productRepo

            )
        {
            _productRepo = productRepo;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productRepo.GetAllAsync());
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productRepo.GetByIdAsync(id));
        }

    }
}
