using System;
using BlockChainLibrary;

namespace tBlockChain
{
    class Program
    {
        static void Main(string[] args)
        {
            string operation = "user1 -> user2 : 500$";
            BlockChain.Create(operation);

            string operation2 = "user1 -> user2 : 200$";
            string operation3 = "user2 -> user1 : 100$";
            string chain = $"{operation2}{Environment.NewLine}{operation3}";

            BlockChain.Create(chain);

            int errors = BlockChain.CheckChain();

            Console.WriteLine($"Number of erros: {errors}");
        }
    }
}