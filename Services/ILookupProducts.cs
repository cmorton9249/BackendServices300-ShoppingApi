using ShoppingApi.Models.Products;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public interface ILookupProducts
    {
        Task<GetProductsResponse> GetSummary();
        Task<GetProductListSummary> GetSummaryList(string category);
        Task<GetProductDetailsResponse> GetbyId(int id);
    }
}