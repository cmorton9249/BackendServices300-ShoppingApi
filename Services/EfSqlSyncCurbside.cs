using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.Models.Curbside;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
	public class EfSqlSyncCurbside : IProvideCurbsideCommands, ILookupCurbsideOrders
	{
		protected readonly ShoppingDataContext _context;

		public EfSqlSyncCurbside(ShoppingDataContext context)
		{
			_context = context;
		}

		public async Task<GetCurbsideResponse> GetById(int id)
		{
			var order = await _context.CurbsideOrders
				.SingleOrDefaultAsync(order => order.Id == id);

			if (order != null)
			{
				return new GetCurbsideResponse
				{
					Id = order.Id,
					For = order.For,
					Items = order.Items.Split(',').Select(int.Parse).ToArray(),
					PickupReadyAt = order.PickupReadyAt
				};
			}

			return null;
		}

		public virtual async Task<GetCurbsideResponse> PlaceOrder(PostCurbsideRequest request)
		{
			// Do the processing (??)
			await Task.Delay(1000 * request.Items.Length);

			var order = new CurbsideOrder
			{
				For = request.For,
				Items = string.Join(",", request.Items),
				PickupReadyAt = DateTime.Now.AddHours(new Random().Next(2,5))
			};
			_context.CurbsideOrders.Add(order);
			await _context.SaveChangesAsync();

			return new GetCurbsideResponse
			{
				Id = order.Id,
				For = order.For,
				Items = order.Items.Split(',').Select(int.Parse).ToArray(),
				PickupReadyAt = order.PickupReadyAt
			};	
		}
	}
}
