using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{/// <summary>
/// represents a purchase menu of a vending machine
/// </summary>
    public class PurchaseMenu
    {
        /// <summary>
        /// Vending machine which is currently running the 
        /// purchase menu
        /// </summary>
        public VendingMachine VM { get; set; }

        /// <summary>
        /// constructor for a purchase menu
        /// </summary>
        /// <param name="vm"></param>
        public PurchaseMenu(VendingMachine vm)
        {
            this.VM = vm;
            this.Display();
        }
        /// <summary>
        /// displays the purchase menu itself 
        /// </summary>
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
                Console.Write("> Pick One: ");

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
                  //current vending machine calls FinishTransaction

                {

                    this.VM.FinishTransaction();
                    //calling dispense message method and passing in
                    //the products purchased info for this particular
                    //vending machine
                    this.DispenseMessage(this.VM.productsPurchased);

                    break;
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
        //menu for method to get money desposited by the user, 
        //passing in the menu selection number, set up within
        //a while loop (ends with user selection action)
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
        //method to get product's slotID from user
        public void GetProduct()
        {
            Console.Clear();
            Console.WriteLine("Enter a slotID for the product you would like to purchase?");
            Console.WriteLine("(Q) Quit to Purchase Menu");

            string choice = Console.ReadLine();

            // IF the user wants to return to main menu
            if (choice.ToLower() == "q")
            {
                // BREAK out of GetProduct to return
                return;
            }

            // IF the product does not exist in inventory
            if (!this.VM.Inventory.ContainsKey(choice))
            {
                // THEN prompt user invalid input
                Console.WriteLine("Invalid slotID. Try Again");
                Console.ReadLine();
                return;
            }

            // Set product from user input
            Product product = this.VM.Inventory[choice];

            // IF product is SOLD OUT
            if (product.Quantity < 1)
            {
                // PROMPT user product SOLD OUT
                Console.WriteLine("Item is SOLD OUT. Try again.");
                Console.ReadLine();
                return;
            }
            // IF not enough money to purchase
            if (this.VM.CurrentBalance < product.Price)
            {
                // PROMPT user not enough money
                Console.WriteLine("Not enough money to purchase. Try again.");
                Console.ReadLine();
                return;
            }

            // Purchase product successful
            this.VM.GiveProduct(choice);
            return;
        }
        //method creates the message that displays a fun purchase-related message 
        //after purchase is made.  Pass in a list of Product Class, named products
        public void DispenseMessage(List<Product> products)
        {//loop through list, checking for produc.Type property and 
            //then run through switch statement to assign proper type-specfic message.
            foreach (Product product in products)
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
            //empty out the list for this purchase
            this.VM.productsPurchased.Clear();
            Console.ReadLine();

        }
    }
}
