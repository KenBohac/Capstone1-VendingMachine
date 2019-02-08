using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// Represents a slot within a VendingMachine. Holds a product and has a maximum quantity.
    /// </summary>
    public class VendingMachineSlot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendingMachineSlot"/> class.
        /// </summary>
        /// <param name="product">the product put into this vending machine slot</param>
        public VendingMachineSlot(Product product)
        {
            int maxQuantity = 5;

            this.HeldProduct = product;
            this.Quantity = maxQuantity;
        }

        /// <summary>
        /// Gets the product held in this vending machine slot
        /// </summary>
        public Product HeldProduct { get; }

        /// <summary>
        /// Gets or sets the number of products remaining in this vending machine slot
        /// </summary>
        public int Quantity { get; set; }
    }
}
