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
            this.Inventory = this.GetStock();
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
        public Dictionary<string, Product> Inventory { get; set; }

        /// <summary>
        /// Adds money to current balance.
        /// </summary>
        /// <param name="money">Amount deposited by user.</param>
        public void DepositMoney(decimal money)
        {
            this.CurrentBalance += money;
            this.LogAction("FEED MONEY:", money, this.CurrentBalance);
        }

        /// <summary>
        /// Sets selected product ready for purchase.
        /// </summary>
        /// <param name="slotID">Slot ID of product.</param>
        public void GiveProduct(string slotID)
        {
            Product product = this.Inventory[slotID];

            if (this.CurrentBalance >= product.Price)
            {
                string logAction = product.Name + " " + slotID.ToUpper();

                product.Quantity--;
                this.CurrentBalance -= product.Price;
                this.LogAction(logAction, this.CurrentBalance + product.Price, this.CurrentBalance);
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
            this.LogAction("GIVE CHANGE:", this.CurrentBalance, 0.00M);

            //Set CurrentBalance to 0.00
            this.CurrentBalance = 0;
            return change;
        }


        /// <summary>
        /// Write the log of user-machine action to external file, log.txt,
        ///according to given format
        /// </summary>
        /// <param name="action">user action, e. g. deposit, purchase, change given</param>
        /// <param name="startingBalance">vending machine's starting balance</param>
        /// <param name="endingBalance">vending machine's ending balance</param>
        private void LogAction(string action, decimal startingBalance, decimal endingBalance)
        {
            // TRY-CATCH to check for I/O exceptions
            try
            {
                //open up new StreamWriter for log.txt file and allow appending
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine($"{System.DateTime.Now}  {action}  {startingBalance:C2}  {endingBalance:C2}");
                }
            }
            //Catch I/O exceptions, e. g. file not found
            catch (IOException ex)
            {
                Console.WriteLine($"Umbrella Corp.: Error with Log.txt file. Action(s) may not have been recorded.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// loads inventory from external file into new vending machine
        /// </summary>
        /// <returns>dictionary representing product slotId's/locations and Products</returns>
        private Dictionary<string, Product> GetStock()
        {
            Dictionary<string, Product> inv = new Dictionary<string, Product>();

            //read in stock from external file line by line; split up csv's, assigning to 
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
            }
            catch (Exception ex)
            {
                // catch streamreader error, e. g. file not found,  Parse.ToDecimal() errors,
                // or datatype mismatch errors
                Console.WriteLine("Umbrella Corp Critical Error: Could not Load Inventory");
                Console.WriteLine("Check correct file name and location.");
                Console.ReadLine();
            }
            return inv;
        }

        /// <summary>
        /// Calculates change based on current remaining balance
        /// </summary>
        /// <returns>integer array representing number of quarter, dimes, nickels
        ///  to be returned to user</returns>
        private int[] CalculateChange()
        {
            //Calculate CurrentBalance remaining from user's deposit 
            //after the purhase in form of quarters, dimes, nickels
            int quarters = (int)(this.CurrentBalance / 0.25M);
            int dimes = (int)((this.CurrentBalance % 0.25M) / 0.1M);
            int nickels = (int)(((this.CurrentBalance % 0.25M) % 0.1M) / 0.05M);

            return new int[] { quarters, dimes, nickels };
        }
    }
}
