using AutoMapper;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;

namespace ShoppingApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductSummaryItem>();
        }
    }
}
