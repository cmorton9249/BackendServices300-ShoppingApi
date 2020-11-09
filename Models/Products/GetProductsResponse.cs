using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models.Products
{
    public class GetProductsResponse
    {
        public int NumberOfProductsInInventory { get; set; }
        public int NumberOfProductsAddedToday { get; set; }
        public int NumberOfProductsOutOfStock { get; set; }

    }
}
