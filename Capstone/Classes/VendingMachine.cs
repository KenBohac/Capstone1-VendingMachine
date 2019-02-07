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
            this.Inventory = new Dictionary<string, Product>();
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
        public void GiveProduct(Product product)
        {
            if (this.CurrentBalance >= product.Price)
            {
                product.Quantity--;
                this.CurrentBalance -= product.Price;
                this.LogAction(product.Name, product.Price);
                this.productsPurchased.Add(product);
            }
        }

        /// <summary>
        /// Dispense product and change, return to main menu, readies machine for next purchase.
        /// </summary>
        public void FinishTransaction()
        {
            //Calculate Change and show to console
            int[] change = this.CalculateChange();
            Console.WriteLine();
            Console.WriteLine($"Your Change: {change[0]} quarters, {change[1]} dimes, and {change[2]} nickels.");
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

            Console.ReadLine();
        }

        private void LogAction(string action, decimal amount)
        {
            // TODO LogAction
        }

        private Dictionary<string, Product> GetStock()
        {
            Dictionary<string, Product> inv = new Dictionary<string, Product>();

            // GetStock
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\Matthew Dunavant\Pairs\c-module-1-capstone-team-3\Capstone\vendingmachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] slotIDNamePriceType = sr.ReadLine().Split('|');

                        string slotID = slotIDNamePriceType[0].ToLower();
                        string productName = slotIDNamePriceType[1];
                        decimal productPrice = decimal.Parse(slotIDNamePriceType[2]);
                        string productType = slotIDNamePriceType[3];

                        inv.Add(slotID, new Product(productName, productPrice, productType));
                        //Console.WriteLine($"{ slotID}: {productName}");
                    }
                }
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                // catch streamreader error
                Console.WriteLine("Umbrella Corp Critical Error: Could not Load Inventory");
                Console.WriteLine("Check correct file name and location.");
                Console.ReadLine();
            }
            return inv;
        }

        private int[] CalculateChange()
        {
            int quarters = (int) (this.CurrentBalance / 0.25M);
            int dimes = (int) ((this.CurrentBalance % 0.25M) / 0.1M);
            int nickels = (int) (((this.CurrentBalance % 0.25M) % 0.1M) / 0.05M);

            return new int[] { quarters, dimes, nickels };
        }
    }
}
