using ShoppingApi.Data;
using System;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
	public interface IGenerateCurbsidePickupTimes
	{
		Task<DateTime> GetPickupDate(CurbsideOrder order);
	}
}
