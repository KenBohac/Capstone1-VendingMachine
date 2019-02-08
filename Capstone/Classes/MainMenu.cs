using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// Represents the main menu of a vending machine
    /// </summary>
    public class MainMenu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// Create a new Main Menu for vending machine
        /// </summary>
        /// <param name="vm">Vending Machine using this menu.</param>
        public MainMenu(VendingMachine vm)
        {
            this.VM = vm;
        }

        /// <summary>
        /// Gets or sets the vending machine on which this menu is operating
        /// </summary>
        public VendingMachine VM { get; set; }

        /// <summary>
        /// Writes out a option menu to the console
        /// </summary>
        public void Display()
        {
            // Main Menu presented to User
            while (true)
            {
                // CLEAR console followed by display options
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("-------------");
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine();
                Console.Write("> Pick One: ");

                // SAVE user choice
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    this.DisplayProducts();
                }
                else if (choice == "2")
                {
                    // Instantiate PurchaseMenu and pass to current vending machine
                    PurchaseMenu pm = new PurchaseMenu(this.VM, this);
                    pm.Display();
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again");
                }
            }
        }

        /// <summary>
        /// Writes out list of products to console
        /// </summary>
        public void DisplayProducts()
        {
            // Display Header categories
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("SlotID" + "Snack".PadLeft(10) + "Price".PadLeft(21) + "Quantity".PadLeft(11));
            Console.WriteLine();

            // FOREACH loop per product in Inventory
            foreach (string key in this.VM.Inventory.Keys)
            {
                VendingMachineSlot vms = this.VM.Inventory[key];
                Product product = vms.HeldProduct;

                // IF this product is in stock
                if (this.VM.Inventory[key].Quantity > 0)
                {
                    Console.WriteLine($"{key.ToUpper().PadLeft(4)} - {product.Name.PadRight(22)} - ${product.Price.ToString().PadRight(7):C2} - {vms.Quantity}");
                }

                // ELSE the product is SOLD OUT
                else
                {
                    Console.WriteLine($"{key.ToUpper().PadLeft(4)} - {product.Name.PadRight(22)} - ${product.Price.ToString().PadRight(7):C2}SOLD OUT");
                }
            }

            // Wait for user to continue
            Console.WriteLine();
            Console.WriteLine("Press any key to return to Menu.");
            Console.ReadKey();
        }
    }
}
