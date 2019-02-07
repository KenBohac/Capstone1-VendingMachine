using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class MainMenu
    {
        public VendingMachine VM { get; set; }
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
            foreach (string key in this.VM.Inventory.Keys)
            {
                if (this.Inventory[key].Quantity > 0)
                {
                    Console.WriteLine($"{key} - {this.VM.Inventory[key].Name} - {this.VM.Inventory[key].Price} - {this.VM.Inventory[key].Quantity}");
                }
                else
                {
                    Console.WriteLine($"{key} - {this.VM.Inventory[key].Name} - {this.VM.Inventory[key].Price} -  SOLD OUT");
                }
            }
        }
        public MainMenu(VendingMachine vm)
        {
            this.VM = vm;
        }
    }
}
