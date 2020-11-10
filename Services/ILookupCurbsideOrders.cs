using System.Threading.Tasks;

namespace ShoppingApi
{
	public interface ILookupCurbsideOrders
	{
		Task<GetCurbsideResponse> GetById(int id);
	}
}