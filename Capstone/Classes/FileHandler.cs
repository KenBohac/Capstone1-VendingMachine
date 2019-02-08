using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileHandler
    {
        public Dictionary<string, VendingMachineSlot> GetStock()
        {
            Dictionary<string, VendingMachineSlot> inv = new Dictionary<string, VendingMachineSlot>();

            // read in stock from external file line by line; split up csv's, assigning to
            // variables, and assigning them to key-value pairs in dictionary
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

                        inv.Add(slotID, new VendingMachineSlot(new Product(productName, productPrice, productType)));
                    }
                }
            }
            catch (Exception)
            {
                // catch streamreader error, e. g. file not found,  Parse.ToDecimal() errors,
                // or datatype mismatch errors
                Console.WriteLine("Umbrella Corp Critical Error: Could not Load Inventory");
                Console.WriteLine("Check correct file name and location.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            return inv;
        }

        public void LogAction(string action, decimal startingBalance, decimal endingBalance)
        {
            // TRY-CATCH to check for I/O exceptions
            try
            {
                // open up new StreamWriter for log.txt file and allow appending
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine($"{System.DateTime.Now}  {action}  {startingBalance:C2}  {endingBalance:C2}");
                }
            }

            // Catch I/O exceptions, e. g. file not found
            catch (IOException)
            {
                Console.WriteLine($"Umbrella Corp.: Error with Log.txt file. Action(s) may not have been recorded.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}
