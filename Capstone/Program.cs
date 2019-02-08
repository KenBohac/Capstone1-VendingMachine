using System;
using Capstone.Classes;

namespace Capstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            MainMenu mm = new MainMenu(vm);
        }
    }
}
