using Microsoft.AspNetCore.Http;
using ShoppingApi.Models.Curbside;
using System.Threading.Tasks;

namespace ShoppingApi
{
	public interface IProvideCurbsideCommands
	{
		Task<GetCurbsideResponse> PlaceOrder(PostCurbsideRequest request);
	}
}