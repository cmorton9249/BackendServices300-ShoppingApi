using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingApi.Data;
using ShoppingApi.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi
{
	public class GrpcPickupEstimator : IGenerateCurbsidePickupTimes
	{
		private readonly IOptions<PickupEstimatorConfiguration> _options;
		private readonly ILogger<GrpcPickupEstimator> _logger;

		public GrpcPickupEstimator(IOptions<PickupEstimatorConfiguration> options, ILogger<GrpcPickupEstimator> logger)
		{
			_options = options;
			_logger = logger;
		}

		public async Task<DateTime> GetPickupDate(CurbsideOrder order)
		{
			var request = new PickupService.PickupRequest
			{
				For = order.For
			};

			request.Items.AddRange(order.Items.Split(',').Select(int.Parse).ToArray());
			try
			{
				_logger.LogInformation("Creating GRPC service");
				var channel = GrpcChannel.ForAddress(_options.Value.Url);
				var client = new PickupService.PickupEstimator.PickupEstimatorClient(channel);

				_logger.LogInformation($"Making the GRPC call for {request.For}");
				var response = await client.GetPickupTimeAsync(request);
				_logger.LogInformation($"Pickup time for {request.For} is {response.PickupTime.ToDateTime().ToShortDateString()}");
				return response.PickupTime.ToDateTime();
			}
			catch (Exception e)
			{
				_logger.LogError("GRPC call has failed");
				_logger.LogError(e.Message);
				// Gotta do real things to handle this error.
				return DateTime.Now.AddYears(1);
			}
		}
	}
}