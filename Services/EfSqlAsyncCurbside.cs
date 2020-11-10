using ShoppingApi.Data;
using ShoppingApi.Models.Curbside;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
	public class EfSqlAsyncCurbside : EfSqlSyncCurbside
	{
		private readonly CurbsideChannel _channel;

		public EfSqlAsyncCurbside(ShoppingDataContext context, CurbsideChannel channel) : base(context)
		{
			_channel = channel;
		}

		public override async Task<GetCurbsideResponse> PlaceOrder(PostCurbsideRequest request)
		{

			var order = new CurbsideOrder
			{
				For = request.For,
				Items = string.Join(",", request.Items),
			};

			_context.CurbsideOrders.Add(order);
			await _context.SaveChangesAsync();
			await _channel.AddCurbside(new CurbsideChannelRequest { OrderId = order.Id });
			
			return new GetCurbsideResponse
			{
				Id = order.Id,
				For = order.For,
				Items = order.Items.Split(',').Select(int.Parse).ToArray(),
			};
		}
	}
}
