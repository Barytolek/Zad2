using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zad2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double previousNumber;
        private string currentOperation;
        private string previousOperationText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonContent = button.Content.ToString();

            if (buttonContent == "C")
            {
                ClearCalculator();
            }
            else if (buttonContent == "=")
            {
                CalculateResult();
            }
            else
            {
                if (buttonContent.StartsWith("√"))
                {
                    PerformUnaryOperation(Math.Sqrt, "√");
                }
                else if (buttonContent == "1/x")
                {
                    PerformUnaryOperation(x => 1 / x, "1/x");
                }
                else if (buttonContent == "x!")
                {
                    PerformUnaryOperation(Factorial, "!");
                }
                else if (buttonContent == "log10")
                {
                    PerformUnaryOperation(Math.Log10, "log10");
                }
                else if (buttonContent == "ln")
                {
                    PerformUnaryOperation(Math.Log, "ln");
                }
                else if (buttonContent == "Floor")
                {
                    PerformUnaryOperation(Math.Floor, "Floor");
                }
                else if (buttonContent == "Ceiling")
                {
                    PerformUnaryOperation(Math.Ceiling, "Ceiling");
                }
                else if (buttonContent == ".") 
                {
                    addDot();
                }
                else
                {
                    if (double.TryParse(buttonContent, out double number))
                    {
                        UpdateDisplay(number);
                    }
                    else
                    {
                        if (buttonContent == "Mod")
                            buttonContent ="%";
                        currentOperation = buttonContent;
                        previousNumber = double.Parse(displayLabel.Content.ToString());
                        previousOperationText = $"{previousNumber} {currentOperation}";
                        displayLabel.Content = "0";
                    }
                }
            }
        }

        private void addDot() 
        {
            string x = displayLabel.Content.ToString();
            if(!x.Contains(","))
            displayLabel.Content = displayLabel.Content+",";
        }
        private void UpdateDisplay(double number)
        {
            if (displayLabel.Content.ToString() == "0")
            {
                displayLabel.Content = number.ToString();
            }
            else
            {
                displayLabel.Content += number.ToString();
            }
        }

        private void ClearCalculator()
        {
            displayLabel.Content = "0";
            previousLabel.Content = "";
            previousNumber = 0;
            currentOperation = null;
            previousOperationText = null;

        }

        private void CalculateResult()
        {
            if (currentOperation != null)
            {
                double currentNumber = double.Parse(displayLabel.Content.ToString());
                double result = 0;

                switch (currentOperation)
                {
                    case "+":
                        result = previousNumber + currentNumber;
                        break;
                    case "-":
                        result = previousNumber - currentNumber;
                        break;
                    case "×":
                        result = previousNumber * currentNumber;
                        break;
                    case "÷":
                        if (currentNumber == 0)
                        {
                            MessageBox.Show("Cannot divide by zero.");
                            ClearCalculator();
                            return;
                        }
                        result = previousNumber / currentNumber;
                        break;
                    case "^":
                        result = Math.Pow(previousNumber, currentNumber);
                        break;
                    case "%":
                        result = previousNumber % currentNumber;
                        break;
                    default:
                        break;
                }

                displayLabel.Content = result.ToString();
                previousLabel.Content = "("+previousNumber+currentOperation.ToString()+currentNumber.ToString()+")";
                previousNumber = result;
                
                currentOperation = null;
                previousOperationText = null;
                
            }
        }

        private void PerformUnaryOperation(Func<double, double> operation, string operationText)
        {
            double currentNumber = double.Parse(displayLabel.Content.ToString());
            double result = operation(currentNumber);
            previousOperationText = $"{operationText}({currentNumber})";
            displayLabel.Content = result.ToString();
            previousLabel.Content = operationText + "(" + currentNumber +")";
        }

        private double Factorial(double n)
        {
            if (n < 0 || n % 1 != 0)
            {
                MessageBox.Show("Factorial is defined only for non-negative integers.");
                ClearCalculator();
                return 0;
            }

            if (n == 0)
            {
                return 1;
            }

            double result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
