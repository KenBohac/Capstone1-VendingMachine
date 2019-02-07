using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
   public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, string type)
        {
            int defaultQuantity = 5;

            this.Name = name;
            this.Price = price;
            this.Type = type;
            this.Quantity = defaultQuantity ;
        }

    }
}
