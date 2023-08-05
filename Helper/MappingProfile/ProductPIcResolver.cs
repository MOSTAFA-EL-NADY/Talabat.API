using AutoMapper;
using Talabat.API.DTOs;
using Talabat.CoreEntities.Entites;
using static System.Net.WebRequestMethods;

namespace Talabat.API.Helper.MappingProfile
{
    public class ProductPIcResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPIcResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}/{source.PictureUrl}";

            return String.Empty;
        }
    }
}
