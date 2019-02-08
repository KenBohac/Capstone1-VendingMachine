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
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="name">name of the product</param>
        /// <param name="price">price of the product</param>
        /// <param name="type">type of the product</param>
        public Product(string name, decimal price, string type)
        {
            int defaultQuantity = 5;

            this.Name = name;
            this.Price = price;
            this.Type = type;
            this.Quantity = defaultQuantity;
        }

        /// <summary>
        /// Gets or sets the name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the snack type. (chip, candy, drink, and gum)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets how many of this product remain
        /// </summary>
        public int Quantity { get; set; }
    }
}
