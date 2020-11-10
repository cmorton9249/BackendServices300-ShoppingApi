using System;

namespace ShoppingApi.Models.Curbside
{
	public class PostCurbsideRequest
	{
		public string For { get; set; }
		public int[] Items { get; set; }
		public DateTime PickupReadyAt { get; set; }
	}
}
