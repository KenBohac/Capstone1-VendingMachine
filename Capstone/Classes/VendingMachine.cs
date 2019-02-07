using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// Represents a Vending Machine
    /// </summary>
    public class VendingMachine
    {
        /// <summary>
        /// Current balance used for purchases
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Dictionary associating the slot ID with a Product. Represents current inventory.
        /// </summary>
        public Dictionary<string, Product> Inventory { get; set; }

        /// <summary>
        /// Creates a new Vending Machine, obtains inventory, and sets default properties.
        /// </summary>
        public VendingMachine()
        {
            this.Inventory = this.GetStock();
            this.CurrentBalance = 0;
        }

        /// <summary>
        /// Adds money to current balance.
        /// </summary>
        /// <param name="money">Amount deposited by user.</param>
        public void DepositMoney(decimal money)
        {
            this.CurrentBalance += money;
            this.LogAction("FEED MONEY:", money);
        }

        /// <summary>
        /// Sets selected product ready for purchase.
        /// </summary>
        /// <param name="slotID">Slot ID of product.</param>
        public void GiveProduct(string slotID)
        {
            if (this.CurrentBalance >= this.Inventory[slotID].Price)
            {
                this.Inventory[slotID].Quantity--;
                this.LogAction(this.Inventory[slotID].Name, this.Inventory[slotID].Price);
            }
        }

        /// <summary>
        /// Dispense product and change, return to main menu, readies machine for next purchase.
        /// </summary>
        public void FinishTransaction()
        {
            // TODO FinishTransaction

        }

        private void LogAction(string action, decimal amount)
        {
            // TODO LogAction
        }

        private Dictionary<string, Product> GetStock()
        {
            Dictionary<string, Product> inv = new Dictionary<string, Product>();

            // TODO GetStock

            return inv;
        }
    }
}
