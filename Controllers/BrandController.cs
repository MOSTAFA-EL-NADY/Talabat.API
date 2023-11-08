using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Error;
using Talabat.CoreEntities.Entites;
using Talabat.CoreEntities.Repositotry;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;
        public BrandController(IGenericRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Brand>>> GetAllBrands()
        {
            var allBrands = _brandRepository.GetAllAsync().Result.ToList();
            if (allBrands.Count == 0)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "No Brands Found"));

            return Ok(allBrands);
        }
    }
}
