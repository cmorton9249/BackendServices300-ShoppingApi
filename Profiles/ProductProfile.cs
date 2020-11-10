using AutoMapper;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;
using System;

namespace ShoppingApi.Profiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile(PricingConfiguration config)
		{
			CreateMap<Product, ProductSummaryItem>()
				.ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => Math.Round(src.UnitPrice * config.Markup, 2)));

			CreateMap<Product, GetProductDetailsResponse>()
				.ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => Math.Round(src.UnitPrice * config.Markup, 2)));

			CreateMap<PostProductRequest, Product>()
				.ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Value))
				.ForMember(dest => dest.InInventory, opt => opt.MapFrom(_ => true));
		}
	}
}
