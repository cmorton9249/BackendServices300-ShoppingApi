using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models.Curbside;

namespace ShoppingApi.Controllers
{

	public class CurbsideController : Controller
	{
		private readonly IProvideCurbsideCommands _curbsideCommands;
		private readonly ILookupCurbsideOrders _curbsideLookups;

		public CurbsideController(IProvideCurbsideCommands curbsideCommands, ILookupCurbsideOrders curbsideLookups)
		{
			_curbsideCommands = curbsideCommands;
			_curbsideLookups = curbsideLookups;
		}

		[HttpGet("/curbsideorders/{id:int}", Name = "curbside#getbyid")]
		public async Task<ActionResult> GetCurbsideOrderById(int id)
		{
			var response = await _curbsideLookups.GetById(id);
			if (response == null)
			{
				return NotFound();
			}

			return Ok(response);
		}

		[HttpPost("/curbsideorders")]
		public async Task<ActionResult> PlaceOrder([FromBody] PostCurbsideRequest curbsideRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var response = await _curbsideCommands.PlaceOrder(curbsideRequest);
			return CreatedAtRoute("curbside#getbyid", new { id = response.Id }, response);
		}
	}
}
