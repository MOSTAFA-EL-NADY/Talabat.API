using Talabat.CoreEntities.Entites;

namespace Talabat.API.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public Decimal price { get; set; }

        public string BrandName { get; set; }

        public string ProductTypeName { get; set; }


    }
}
