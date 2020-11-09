﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models.Products
{
    public class GetProductDetailsResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
