using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class MainMenu
    {
        public Dictionary<string, Product> Inventory { get; set; }
        public void Display()
        {
            ///Main Menu presented to User
            ///Don't forget to add Matt's Umbrella Corp. Word Art
            ///
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
        }
        public void DisplayProducts()
        {
            foreach (string key in this.Inventory.Keys)
            {
                if (this.Inventory[key].Quantity > 0)
                {
                    Console.WriteLine($"{key} - {this.Inventory[key].Name} - {this.Inventory[key].Price} - {this.Inventory[key].Quantity}");
                }
                else
                {
                    Console.WriteLine($"{key} - {this.Inventory[key].Name} - {this.Inventory[key].Price} -  SOLD OUT");
                }
            }
        }
        public MainMenu(VendingMachine vm)
        {
            this.Inventory = vm.Inventory;
        }
    }
}
