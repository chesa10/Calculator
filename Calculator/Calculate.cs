using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculate : ICalculatorServices
    {
        public double Results { get; set; }
        public string Operation { get; set; }
        public bool IsOperatorUsed { get; set; }
        public string LastEnteredValue { get; set; }

        public Calculate()
        {
            this.Results = 0;
            this.Operation = "";
            this.IsOperatorUsed = false;
            this.LastEnteredValue = "0";
        }

        public bool IsDevidingByZero(string number)
        {
            if (number == "0")
                return true;
            else
                return false;
        }

        public bool IsPointEntered(string entered, string entry)
        {
            if (entered == ",")
            {
                if (!entry.Contains(","))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool MaximumDisplayReached(string number)
        {
            if (number.Length > 8)
                return true;
            else
                return false;
        }

        public string PerformCalculations(double firstValue, string secondValue)
        {
            string results = "0";

            switch (this.Operation)
            {
                case "+":
                    return results = (firstValue + Double.Parse(secondValue)).ToString();
                case "-":
                    return results = (firstValue - Double.Parse(secondValue)).ToString();
                case "*":
                    return results = (firstValue * Double.Parse(secondValue)).ToString();
                case "/":
                     return results = (firstValue / Double.Parse(secondValue)).ToString();
                default:
                    return results;
            }
        }
    }
}
