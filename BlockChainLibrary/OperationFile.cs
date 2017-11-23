using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockChainLibrary
{
    public static class OperationFile
    {
        private static readonly string pathToOperations = $@"{AppDomain.CurrentDomain.BaseDirectory}Operations";
        private static readonly string fileExtension = "txt";
        private static readonly string fileName = "opr";

        public static int GetLastOperationNumber()
        {
            string[] files = Directory.GetFiles(pathToOperations, $"*.{fileExtension}");
            int operationFiles = -1;

            if (files.Length != 0)
                operationFiles = files.Select(Path.GetFileName)
                                         .Select(x => Regex.Match(x, @"\d+").Value)
                                         .Select(int.Parse)
                                         .Max();

            return operationFiles;
        }

        public static string GetFilePath(int operationNumber)
        {
            string newFileName = $"{fileName}{operationNumber}";
            string pathToTheNewFile = $"{pathToOperations}\\{newFileName}.{fileExtension}";

            return pathToTheNewFile;
        }

        public static FileInfo[] GetFilesFromDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(pathToOperations);

            return dir.GetFiles();
        }

        public static string GetLastLine(string path)
        {
            var lines = File.ReadAllLines(path);

            return lines.Last();
        }

        public static string GetLastFilePath()
        {
            int lastOperationNumber = GetLastOperationNumber();
            string lastOperationFilePath = null;

            if (lastOperationNumber > -1)
                lastOperationFilePath = GetFilePath(lastOperationNumber);

            return lastOperationFilePath;
        }
    }
}
