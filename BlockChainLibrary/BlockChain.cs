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

                string SHAOfPrevOperation = GetHashOperationFile(OperationFile.GetLastFilePath());
                string contents = $"{operation}{Environment.NewLine}{SHAOfPrevOperation}";

                File.WriteAllText(newFilePath, contents);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public static int CheckChain()
        {
            var fInfos = OperationFile.GetFilesFromDirectory();
            int errorNum = 0;

            for (int i = 0; i < fInfos.Length - 1; i++ )
            {
                string hash = GetHashOperationFile(fInfos[i].FullName);
                string hashInNextFile = OperationFile.GetLastLine(fInfos[i + 1].FullName);

                if (!hash.Equals(hashInNextFile)) errorNum++;
            }

            return errorNum;
        }

        private static string GetHashOperationFile(string path)
        {
            if (!ReferenceEquals(path, null))
            {
                FileStream prevOperationFile = File.Open(path, FileMode.Open);

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
