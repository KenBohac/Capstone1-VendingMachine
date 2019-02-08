using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes; 
namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [DataTestMethod]
        [DataRow(0.9, new int[] {3,1,1 })]
        [DataRow(.5, new int[] { 2, 0, 0 })]
        [DataRow(.0, new int[] { 0, 0, 0 })]

        public void FinishTransactionTests(double input, int [] expectedOutput)
        {

            //Arrange
            VendingMachine vm = new VendingMachine();

            //Act
            vm.DepositMoney((decimal)input);
            

            //Assert
            CollectionAssert.AreEqual(expectedOutput, vm.FinishTransaction() );
                

        }
    }
}
