using AutoMapper;
using Talabat.API.DTOs;
using Talabat.CoreEntities.Entites;

namespace Talabat.API.Helper.MappingProfile
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(des => des.PictureUrl, o => o.MapFrom<ProductPIcResolver>());


        }
    }
}
