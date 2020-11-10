using System;

namespace ShoppingApi
{
	public class GetCurbsideResponse
	{
		public object Id { get; internal set; }
		public string For { get; internal set; }
		public int[] Items { get; internal set; }
		public DateTime? PickupReadyAt { get; internal set; }
	}
}