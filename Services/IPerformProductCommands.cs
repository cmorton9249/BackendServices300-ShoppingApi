using ShoppingApi.Models.Products;
using System.Threading.Tasks;

namespace ShoppingApi
{
    public interface IPerformProductCommands
    {
        Task<GetProductDetailsResponse> AddProduct(PostProductRequest productToAdd);
    }
}