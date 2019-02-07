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
        /// A list of successfully purchased products in a single transaction. 
        /// used in LogAction, generating and updating sales report, and in FinishTransaction()
        /// </summary>
        public List<Product> productsPurchased { get; set; }

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
            this.productsPurchased = new List<Product>();
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
                this.CurrentBalance -= this.Inventory[slotID].Price;
                this.LogAction(this.Inventory[slotID].Name, this.Inventory[slotID].Price);
            }
        }

        /// <summary>
        /// Dispense product and change, return to main menu, readies machine for next purchase.
        /// </summary>
        public void FinishTransaction()
        {
            //Calculate Change and show to console
            int[] change = this.CalculateChange();
            Console.Write($"Your Change: {change[0]} quarters, {change[1]} dimes, and {change[2]} nickels.");

            //Set CurrentBalance to 0.00
            this.CurrentBalance = 0;

            //LogAction i. e., change given

            //Adds give change msg and current balance to Log.txt output 
            //to record action

            this.LogAction("GIVE CHANGE:", this.CurrentBalance);

            //Empty out/ Clear out list of all products purchased



            //Based on choice, display appropriate message to console for user

            foreach (Product product in productsPurchased)
            {
                switch (product.Type.ToLower())
                {
                    case "chip":

                        Console.WriteLine("Crunch Crunch, Yum!");
                        break;

                    case "candy":

                        Console.WriteLine("Munch Munch, Yum!");
                        break;

                    case "drink":

                        Console.WriteLine("Glug Glug Yum!");
                        break;
                    case "gum":

                        Console.WriteLine("Chew Chew Yum!");
                        break;

                    default:
                        break;
                }
            }

            this.productsPurchased.Clear();

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
