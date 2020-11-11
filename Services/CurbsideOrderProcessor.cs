using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingApi.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
	public class CurbsideOrderProcessor: BackgroundService
	{
		private readonly ILogger<CurbsideOrderProcessor> _logger;
		private readonly CurbsideChannel _channel;
		private readonly IServiceProvider _serviceProvider;

		public CurbsideOrderProcessor(ILogger<CurbsideOrderProcessor> logger, CurbsideChannel channel, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_channel = channel;
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await foreach(var order in _channel.ReadAllAsync())
			{
				_logger.LogInformation($"got an order for {order.OrderId}");
				// look it up
				// process it
				// get the estimated pickup time
				// write it to the DB
				using var scope = _serviceProvider.CreateScope();
				var context = scope.ServiceProvider.GetRequiredService<ShoppingDataContext>();

				var savedOrder = await context.CurbsideOrders.SingleOrDefaultAsync(c => c.Id == order.OrderId);
				var items = savedOrder.Items.Split(',');
				foreach(var item in items)
				{
					await Task.Delay(1000); // It's important!
					_logger.LogInformation($"Processed item {item} for order {order.OrderId}");
				}

				var pickupGenerator = scope.ServiceProvider.GetRequiredService<IGenerateCurbsidePickupTimes>();
				savedOrder.PickupReadyAt = await pickupGenerator.GetPickupDate(savedOrder);
				await context.SaveChangesAsync();
			}
		}
	}
}
