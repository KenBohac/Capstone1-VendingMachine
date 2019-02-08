using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileHandler : IReaderWriter
    {
        public FileHandler(VendingMachine vm)
        {
            this.VM = vm;
        }

        public VendingMachine VM { get; }

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

        public void UpdateSalesReport(Product product)
        {
            // DECLARE price of product
            decimal productPrice = product.Price;

            // DECLARE list of strings representing each line of the sales report
            List<string> salesReport = new List<string>();

            // IF a salesreport does not exist
            if (!File.Exists("SalesReport.txt"))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter("SalesReport.txt"))
                    {
                        foreach (VendingMachineSlot vms in this.VM.Inventory.Values)
                        {
                            Product recordedProduct = vms.HeldProduct;
                            if (recordedProduct != product)
                            {
                                sw.WriteLine(recordedProduct.Name + "|" + "0");
                            }
                            else
                            {
                                sw.WriteLine(product.Name + "|" + "1");
                            }
                        }

                        sw.WriteLine();
                        sw.WriteLine($"**TOTAL SALES** {productPrice:C2}");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Creating Sales Report failed.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
            else
            {
                // READ and save the current sales report
                try
                {
                    using (StreamReader sr = new StreamReader("SalesReport.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            salesReport.Add(sr.ReadLine());
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Reading Sales Report failed.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }

                // FOR each line in sales report
                for (int i = 0; i < salesReport.Count; i++)
                {
                    // IF the line contains the product name
                    if (salesReport[i].Contains(product.Name))
                    {
                        // SPLIT the line between name and sales count
                        string[] lineSplit = salesReport[i].Split('|');

                        // PARSE sales count into a int variable
                        int numProductSold = int.Parse(lineSplit[1]);

                        // INCREMENT sales count
                        numProductSold++;

                        // RECREATE line for sales report
                        salesReport[i] = lineSplit[0] + "|" + numProductSold;

                        // DONT continue searching for products
                        break;
                    }
                }

                // SPLIT final line of sales report between TOTAL SALES and number representing total sales
                string[] totalSalesLine = salesReport[salesReport.Count - 1].Split('$');

                // PARSE number representing total sales and add the price of the product purchased
                decimal totalSales = decimal.Parse(totalSalesLine[1]) + productPrice;

                // RECREATE final line of sales report
                salesReport[salesReport.Count - 1] = $"{totalSalesLine[0]}{totalSales:C2}";

                // OVERWRITE new sales report in same file
                try
                {
                    using (StreamWriter sw = new StreamWriter("SalesReport.txt"))
                    {
                        foreach (string line in salesReport)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Sales report update failed.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
    }
}
