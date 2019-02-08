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
            this.LogAction("FEED MONEY:", money, this.CurrentBalance );
        }

        /// <summary>
        /// Sets selected product ready for purchase.
        /// </summary>
        /// <param name="slotID">Slot ID of product.</param>
        public void GiveProduct(string slotID )
        {
            Product product = this.Inventory[slotID];

            if (this.CurrentBalance >= product.Price)
            {
                string logAction = product.Name + " " + slotID.ToUpper();
                
                product.Quantity--;
                this.CurrentBalance -= product.Price;
                this.LogAction(logAction, CurrentBalance + product.Price, CurrentBalance);
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
           
            //LogAction i. e., change given

            //Adds give change msg and current balance to Log.txt output 
            //to record action

            this.LogAction("GIVE CHANGE:", this.CurrentBalance, 0.00M);
            //Set CurrentBalance to 0.00
            this.CurrentBalance = 0;

           
        }
        //Write the log of user-machine action to external file, log.txt,
        //according to given format
        private void LogAction(string action, decimal startingBalance, decimal endingBalance)
        {
            // Try-Catch to check for I/O exceptions
            try
            {
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine($"{System.DateTime.Now}  {action}  {startingBalance:C2}  {endingBalance:C2}");

                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Umbrella Corp.: Error with Log.txt file. Action(s) may not have been recorded.");

            }
        }

        private Dictionary<string, Product> GetStock()
        {
            Dictionary<string, Product> inv = new Dictionary<string, Product>();

            // GetStock
            //read in stock from external file; split up csv's, assigning to 
            //variables, and assigning them to key-value pairs in dictionary
            try
            {
                using (StreamReader sr = new StreamReader(@"vendingmachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] slotIDNamePriceType = sr.ReadLine().Split('|');

                        string slotID = slotIDNamePriceType[0].ToLower();
                        string productName = slotIDNamePriceType[1];
                        decimal productPrice = decimal.Parse(slotIDNamePriceType[2]);
                        string productType = slotIDNamePriceType[3];

                        inv.Add(slotID, new Product(productName, productPrice, productType));
                        
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
        {//Calculate CurrentBalance remaining from user's deposit 
        //after the purhase  in form of quarters, dimes, nickels
            int quarters = (int)(this.CurrentBalance / 0.25M);
            int dimes = (int)((this.CurrentBalance % 0.25M) / 0.1M);
            int nickels = (int)(((this.CurrentBalance % 0.25M) % 0.1M) / 0.05M);

            return new int[] { quarters, dimes, nickels };
        }
    }
}
