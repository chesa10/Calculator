using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public interface ITextFileHandlerServices
    {
        void AddHistory(string lines);
        void ClearHistory();
        string[] GellAllLines();
    }
}
