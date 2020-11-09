using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILookupProducts _productsService;

        public ProductsController(ILookupProducts productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("/products")]
        public async Task<ActionResult> GetProducts([FromQuery]string category = null)
        {
            if (category == null)
            {
                GetProductsResponse response = await _productsService.GetSummary();
                return Ok(response);
            }
            else
            {
                GetProductListSummary response = await _productsService.GetSummaryList(category);
                return Ok(response);
            }            
        }
    }
}
