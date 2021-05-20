using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public class TextFileHandler : ITextFileHandlerServices
    {
        public TextFileHandler(string filePath)
        {
            this.FilePath = filePath;
            if (!File.Exists(FilePath))
            {
                using (StreamWriter writer = File.CreateText(this.FilePath)) { }
            }
        }
        public void AddHistory(string lines)
        {
            File.AppendAllText(FilePath, lines + Environment.NewLine);
        }
        public void ClearHistory()
        {
            File.WriteAllText(FilePath, string.Empty);
        }
        public string[] GellAllLines()
        {
            string[] lines = System.IO.File.ReadAllLines(FilePath);

            return lines;
        }

        private string FilePath;
    }
}
