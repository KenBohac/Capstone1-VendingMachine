using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// Represents a Vending Machine product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The price of the product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The snack type. (chip, candy, drink, and gum)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Represents how many of this product remain
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="type"></param>
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
