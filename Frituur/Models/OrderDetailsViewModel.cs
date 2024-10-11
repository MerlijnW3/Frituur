using System;
using System.Collections.Generic;

namespace Frituur.Models
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductQuantity> Products { get; set; } = new List<ProductQuantity>();
        public double TotalCost { get; set; }
    }

    public class ProductQuantity
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; } // Nullable discount for each product
    }
}
