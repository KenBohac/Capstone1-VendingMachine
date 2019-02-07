using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class PurchaseMenu
    {
        public VendingMachine VM { get; set; }

        public PurchaseMenu(VendingMachine vm)
        {
            this.VM = vm;
            this.Display();
        }

        public void Display()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine("(Q) Quit to Main Menu");
                Console.WriteLine($"Current Money Provided: {this.VM.CurrentBalance:C2}");
                Console.WriteLine("> Pick One: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    this.GetMoney();
                }
                else if (choice == "2")
                {
                    this.GetProduct();
                }
                else if (choice == "3")
                {
                    this.VM.FinishTransaction();
                }
                else if(choice.ToLower()== "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again.");
                }
            }
        }

        public void GetMoney()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("How much are you depositing?");
                Console.WriteLine($"Current Money Provided: {this.VM.CurrentBalance:C2}");
                Console.WriteLine("(1) $1.00");
                Console.WriteLine("(2) $2.00");
                Console.WriteLine("(5) $5.00");
                Console.WriteLine("(10) $10.00");
                Console.WriteLine("(Q) Quit");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    this.VM.DepositMoney(1);
                }
                else if (choice == "2")
                {
                    this.VM.DepositMoney(2);
                }
                else if (choice == "5")
                {
                    this.VM.DepositMoney(5);
                }
                else if (choice == "10")
                {
                    this.VM.DepositMoney(10);
                }
                else if (choice.ToLower() == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again.");
                }
            }
        }

        public void GetProduct()
        {
            Console.WriteLine("Enter a slotID for the product you would like to purchase?");
            Console.WriteLine("(Q) Quit to Purchase Menu");

            string choice = Console.ReadLine();

            // IF the user wants to return to main menu
            if (choice.ToLower() == "q")
            {
                // BREAK out of GetProduct to return
                return;
            }
            // ELSE IF the product is in Inventory AND user has enough money to pay AND the product is in stock
            else if (!(this.VM.Inventory[choice] == null) && this.VM.CurrentBalance >= this.VM.Inventory[choice].Price && this.VM.Inventory[choice].Quantity > 0)
            {
                // THEN dispense product (method will also log purchase)
                this.VM.GiveProduct(choice);
            }
            // ELSE IF the product is in inventory AND user does not have enough money
            else if (!(this.VM.Inventory[choice] == null) && this.VM.CurrentBalance < this.VM.Inventory[choice].Price)
            {
                // PROMPT user "not enough money"
                Console.WriteLine("Not enough money to purchase. Try again.");
            }
            // ELSE IF the product is in inventory AND is SOLD OUT
            else if (!(this.VM.Inventory[choice] == null) && this.VM.Inventory[choice].Quantity < 1)
            {
                // PROMPT user "SOLD OUT"
                Console.WriteLine("Item is SOLD OUT. Try again.");
            }
            // ELSE
            else
            {
                // PROMPT user invalid input and return to purchase menu
                Console.WriteLine("Invalid slotID. Try Again");
            }
        }
    }
}
