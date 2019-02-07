using System;
using Capstone.Classes;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            MainMenu mm = new MainMenu(vm);
        }
    }
}
