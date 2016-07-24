using System;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Configuration;

namespace WebMoney.XmlInterfaces.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Initializer initializer;

            //// Option 1: initialization from code
            //initializer =
            //    new Initializer(WmId.Parse("227583964705"),
            //        new KeeperKey(
            //            "<RSAKeyValue><Modulus>uTofMnUSbGMU+BRUBEiKk/PETBivydURnJu/r1NfX/GJicQlGEwBgcl293acLLFe1wfnL3ULZPyuDkOQDC1fvokH</Modulus><D>kyWOQCP0jRYxERtJo2LgW8bBB9GPTrofkRTcUSoR1nUxXTlkXssyHJHqTXKj+D9vnxlIdPnYjAzIxVdyqWerFZcB</D></RSAKeyValue>"));

            // Option 1: initialization from App.config
            initializer = new Configurator();

            // initializer.Apply(); // Use this if you need a global configuration (for desktop single-threaded applications).

            DateTime fromTime = new DateTime(2016, 07, 1, 0, 0, 0);

            var transferFilter = new TransferFilter(Purse.Parse("U415434285082"), fromTime, fromTime.AddMonths(1));
            transferFilter.Initializer = initializer; // Request initialization.

            var transferRegister = transferFilter.Submit();

            foreach (var transfer in transferRegister.TransferList)
            {
                Console.WriteLine(transfer.Amount);
                Console.WriteLine(transfer.Description);
            }
        }
    }
}
