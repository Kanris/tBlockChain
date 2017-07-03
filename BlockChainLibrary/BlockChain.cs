using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockChainLibrary
{
    public static class BlockChain
    {
        private static SHA256 sha256 = SHA256Managed.Create();

        public static void Create(string operation)
        {
            try
            {
                int operationNumber = OperationFile.GetLastOperationNumber() + 1;
                string newFilePath = OperationFile.GetFilePath(operationNumber);

                string SHAOfPrevOperation = HashLastOperationFile();
                string contents = $"{operation}{Environment.NewLine}{SHAOfPrevOperation}";

                File.WriteAllText(newFilePath, contents);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static string HashLastOperationFile()
        {
            string lastOperationFilePath = OperationFile.GetLastFilePath();

            if (!ReferenceEquals(lastOperationFilePath, null))
            {
                FileStream prevOperationFile = File.Open(lastOperationFilePath, FileMode.Open);

                prevOperationFile.Position = 0;

                var hashValue = sha256.ComputeHash(prevOperationFile);
                var hashString = ReadebleByteArray(hashValue);

                prevOperationFile.Close();

                return hashString;
            }

            return "";
        }

        private static string ReadebleByteArray(byte[] array)
        {
            string sha = "";

            for (int i = 0; i < array.Length; i++)
            {
                sha += String.Format("{0:X2}", array[i]);
                if ((i % 4) == 3) sha += " ";
            }

            return sha;
        }
    }
}
