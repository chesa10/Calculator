using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface ICalculatorServices
    {
        bool IsDevidingByZero(string number);
        bool IsPointEntered(string entered, string entry);
        bool MaximumDisplayReached(string number);
        string PerformCalculations(double firstValue, string secondValue);
        double Results { get; set; }
        string Operation { get; set; }
        bool IsOperatorUsed { get; set; }
        string LastEnteredValue { get; set; }
    }
}
