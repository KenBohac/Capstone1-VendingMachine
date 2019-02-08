using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public interface IReaderWriter
    {
        void LogAction(string action, decimal startingBalance, decimal endingBalance);

        Dictionary<string, VendingMachineSlot> GetStock();
    }
}
