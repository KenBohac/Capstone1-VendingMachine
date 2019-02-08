using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// Represents a Vending Machine
    /// </summary>
    public class VendingMachine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendingMachine"/> class.
        /// Creates a new Vending Machine, obtains inventory, and sets default properties.
        /// </summary>
        public VendingMachine()
        {
            FileHandler fh = new FileHandler(this);
            this.FH = fh;
            this.Inventory = fh.GetStock();
            this.CurrentBalance = 0;
            this.ProductsPurchased = new List<Product>();
        }

        /// <summary>
        /// Gets or sets a list of successfully purchased products in a single transaction.
        /// used in LogAction, generating and updating sales report, and in FinishTransaction()
        /// </summary>
        public List<Product> ProductsPurchased { get; set; }

        /// <summary>
        /// Gets or sets current balance used for purchases
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Gets or sets dictionary associating the slot ID with a Product. Represents current inventory.
        /// </summary>
        public Dictionary<string, VendingMachineSlot> Inventory { get; set; }

        public FileHandler FH { get; }

        /// <summary>
        /// Adds money to current balance.
        /// </summary>
        /// <param name="money">Amount deposited by user.</param>
        public void DepositMoney(decimal money)
        {
            this.CurrentBalance += money;
            this.FH.LogAction("FEED MONEY:", money, this.CurrentBalance);
        }

        /// <summary>
        /// Sets selected product ready for purchase.
        /// </summary>
        /// <param name="slotID">Slot ID of product.</param>
        public void GiveProduct(string slotID)
        {
            VendingMachineSlot vms = this.Inventory[slotID];
            Product product = vms.HeldProduct;

            if (this.CurrentBalance >= product.Price)
            {
                string logAction = product.Name + " " + slotID.ToUpper();

                vms.Quantity--;
                this.CurrentBalance -= product.Price;
                this.FH.LogAction(logAction, this.CurrentBalance + product.Price, this.CurrentBalance);
                this.ProductsPurchased.Add(product);
            }
        }

        /// <summary>
        /// Dispense product and change, return to main menu, readies machine for next purchase.
        /// </summary>
        /// <returns>int[] representing change due to user in quarters, dimes, and nickels</returns>
        public int[] FinishTransaction()
        {
            // Calculate Change and show to console
            int[] change = this.CalculateChange();

            // LogAction i. e., change given.
            // Adds a "give change msg" and current balance to Log.txt output
            // to record action
            this.FH.LogAction("GIVE CHANGE:", this.CurrentBalance, 0.00M);

            // Set CurrentBalance to 0.00
            this.CurrentBalance = 0;
            return change;
        }

        /// <summary>
        /// Calculates change based on current remaining balance
        /// </summary>
        /// <returns>integer array representing number of quarter, dimes, nickels
        ///  to be returned to user</returns>
        private int[] CalculateChange()
        {
            // Calculate CurrentBalance remaining from user's deposit
            // after the purhase in form of quarters, dimes, nickels
            int quarters = (int)(this.CurrentBalance / 0.25M);
            int dimes = (int)((this.CurrentBalance % 0.25M) / 0.1M);
            int nickels = (int)(((this.CurrentBalance % 0.25M) % 0.1M) / 0.05M);

            return new int[] { quarters, dimes, nickels };
        }
    }
}
