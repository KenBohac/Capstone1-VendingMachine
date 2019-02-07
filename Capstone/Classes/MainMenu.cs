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
        /// The vending machine on which this menu is operating
        /// </summary>
        public VendingMachine VM { get; set; }

        /// <summary>
        /// Writes out a option menu to the console
        /// </summary>
        public void Display()
        {
            ///Main Menu presented to User
            while (true)
            {
                // CLEAR console followed by display options
                Console.Clear();
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
               
                Console.Write("> Pick One: ");
                
                // SAVE user choice
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    this.DisplayProducts();
                }

                else if (choice == "2")
                {
                    // Instantiate purchase menu and pass current Vending Machine
                    PurchaseMenu pm = new PurchaseMenu(this.VM);
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
            Console.WriteLine("SlotID" + "Snack".PadLeft(10) + "Price".PadLeft(21) + "Quantity".PadLeft(11));

            // FOREACH product in Inventory
            foreach (string key in this.VM.Inventory.Keys)
            {
                // IF this product is in stock
                if (this.VM.Inventory[key].Quantity > 0)
                {
                    Console.WriteLine($"{key.ToUpper().PadLeft(4)} - {this.VM.Inventory[key].Name.PadRight (22)} - ${this.VM.Inventory[key].Price.ToString().PadRight(7):C2} - {this.VM.Inventory[key].Quantity}");
                }
                // ELSE the product is SOLD OUT
                else
                {
                    Console.WriteLine($"{key.ToUpper().PadLeft(4)} - {this.VM.Inventory[key].Name.PadRight(22)} - ${this.VM.Inventory[key].Price.ToString().PadRight(7):C2} -  SOLD OUT");
                }

            }

            // Wait for user to continue
            Console.ReadLine();
        }

        /// <summary>
        /// Create a new Main Menu for vending machine
        /// </summary>
        /// <param name="vm">Vending Machine using this menu.</param>
        public MainMenu(VendingMachine vm)
        {
            this.VM = vm;
            this.Display();
        }
    }
}
