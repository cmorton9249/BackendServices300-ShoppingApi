using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILookupProducts _productsService;
        private readonly IPerformProductCommands _productCommands;

        public ProductsController(ILookupProducts productsService, IPerformProductCommands productCommander)
        {
            _productsService = productsService;
            _productCommands = productCommander;
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

        [HttpGet("/products/{id:int}", Name ="products#getbyid")]
        public async Task<ActionResult> GetAProductById(int id)
        {
            GetProductDetailsResponse response = await _productsService.GetbyId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("/products")]
        public async Task<ActionResult> AddProduct([FromBody] PostProductRequest productToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GetProductDetailsResponse response = await _productCommands.AddProduct(productToAdd);
            return CreatedAtRoute("products#getbyid", new { id = response.Id }, response);
        }
    }
}
