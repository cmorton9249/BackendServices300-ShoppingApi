using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
	public class PricingConfiguration
	{
		public readonly string SectionName = "Pricing";
		public decimal Markup { get; set; }
	}
}
