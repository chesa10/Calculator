using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator;
using FileHandler;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ICalculatorServices calculator;
        ITextFileHandlerServices textFileHandler;
        public Form1()
        {
            InitializeComponent();
        }

        private void Num_Click(object sender, EventArgs e)
        {
            if ((txtEntries.Text == "0") || (calculator.IsOperatorUsed))
            {
                txtEntries.Clear();
            }

            calculator.IsOperatorUsed = false;
            Button buttomNum = (Button) sender;
            if (calculator.IsPointEntered(buttomNum.Text, txtEntries.Text))
            {
                txtEntries.Text = txtEntries.Text += buttomNum.Text;
                calculator.LastEnteredValue = txtEntries.Text;
            }
        }

        private void Operation_Click(object sender, EventArgs e)
        {
            Button operationButton = (Button)sender;

            if (calculator.Results != 0)
            {
                calculator.Operation = operationButton.Text;
                btnEquals.PerformClick();
                calculator.IsOperatorUsed = true;
                lblEntry.Text = $"{calculator.Results} {calculator.Operation}";
            }
            else
            {
                calculator.Operation = operationButton.Text;
                calculator.Results = Double.Parse(txtEntries.Text);
                calculator.IsOperatorUsed = true;
                lblEntry.Text = $"{calculator.Results} {calculator.Operation}";

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEntries.Text = "0";
            calculator.LastEnteredValue = "0";
            lblEntry.Text = string.Empty;
            calculator.Results = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEntries.Text = "0";
            calculator.LastEnteredValue = "0";
            lblEntry.Text = string.Empty;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(calculator.Operation))
            {
                string results = calculator.PerformCalculations(calculator.Results, calculator.LastEnteredValue);
                if (calculator.MaximumDisplayReached(results))
                {
                    MessageBox.Show("You reached the maximum display length. Results will be truncated.");
                    results = results.Substring(0, 8);
                }

                txtEntries.Text = results;
                lblEntry.Text = $"{calculator.Results} {calculator.Operation} { calculator.LastEnteredValue} = ";
                calculator.Results = double.Parse(txtEntries.Text);

                // Add to history List box;
                textFileHandler.AddHistory($"{lblEntry.Text} {txtEntries.Text}");
                lstHistory.Items.Add($"{lblEntry.Text} {txtEntries.Text}"); 
            }
        }

        private void btnMplus_Click(object sender, EventArgs e)
        {
            var currentValue = double.Parse(Properties.Settings.Default.MemoryValue == string.Empty ? "0" : Properties.Settings.Default.MemoryValue);
            currentValue += double.Parse(txtEntries.Text);
            Properties.Settings.Default.MemoryValue = currentValue.ToString();
            SaveProperties();
        }

        private void btnMminus_Click(object sender, EventArgs e)
        {
            var currentValue = double.Parse(Properties.Settings.Default.MemoryValue == string.Empty ? "0" : Properties.Settings.Default.MemoryValue);
            currentValue -= double.Parse(txtEntries.Text);
            Properties.Settings.Default.MemoryValue = currentValue.ToString();
            SaveProperties();
        }

        private void BtnMemoryRemember_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.MemoryValue == string.Empty)
            {
                txtEntries.Text = "0";
                return;
            }
            txtEntries.Text = Properties.Settings.Default.MemoryValue;
        }

        private void btnMemoryClear_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MemoryValue = string.Empty;
            SaveProperties();
        }

        private void SaveProperties()
        {
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            calculator = new Calculate();
            textFileHandler = new TextFileHandler(@"C:\Temp\HistoryData.txt");
            lstHistory.Items.AddRange(textFileHandler.GellAllLines());
        }

        private void lstHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHistory.SelectedItem != null)
            {
                var selectedValue = lstHistory.SelectedItem.ToString();
                lblEntry.Text = $"{selectedValue.Substring(0, selectedValue.LastIndexOf("=") + 1)}";
                txtEntries.Text = $"{selectedValue.Substring(selectedValue.LastIndexOf("= ") + 2).Trim()}"; 
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            textFileHandler.ClearHistory();
            lstHistory.Items.Clear();
        }

        private void txtEntries_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
