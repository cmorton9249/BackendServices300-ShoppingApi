using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class EfSqlShopping : ILookupProducts, IPerformProductCommands
    {
        private readonly ShoppingDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;

        public EfSqlShopping(ShoppingDataContext context, IMapper mapper, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapper;
            _mapperConfig = mapperConfig;
        }

        public async Task<GetProductDetailsResponse> AddProduct(PostProductRequest productToAdd)
        {
            var product = _mapper.Map<Product>(productToAdd);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetProductDetailsResponse>(product);
        }

        public async Task<GetProductDetailsResponse> GetbyId(int id)
        {
            return await _context.GetItemsInInventory()
                .Where(p => p.Id == id)
                .ProjectTo<GetProductDetailsResponse>(_mapperConfig)
                .SingleOrDefaultAsync();
        }

        public async Task<GetProductsResponse> GetSummary()
        {
            var response = new GetProductsResponse();
            var countInInventory = await _context.Products.CountAsync(p => p.InInventory);
            var countOutOfInventory = await _context.Products.CountAsync(p => !p.InInventory);

            response.NumberOfProductsInInventory = countInInventory;
            response.NumberOfProductsOutOfStock = countOutOfInventory;
            response.NumberOfProductsAddedToday = new Random().Next(12, 300);
            return response;
        }

        public async Task<GetProductListSummary> GetSummaryList(string category)
        {
            var list = await _context.GetItemsFromCategory(category)
                .ProjectTo<ProductSummaryItem>(_mapperConfig)
                .ToListAsync();

            var response = new GetProductListSummary
            {
                Data = list,
                Category = category,
                Count = list.Count()
            };

            return response;
        }
    }
}
