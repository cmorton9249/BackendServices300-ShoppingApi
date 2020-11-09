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

            CreateMap<Product, GetProductDetailsResponse>();

            CreateMap<PostProductRequest, Product>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Value))
                .ForMember(dest => dest.InInventory, opt => opt.MapFrom(_ => true));
        }
    }
}
