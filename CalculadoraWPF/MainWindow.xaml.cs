using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculadoraWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal operator1 = 0, operator2 = 0, result = 0;
        int currentStatus = 0; // 0:Input operator1 +(user press symbol), 1:Waiting for input operator2 +(user press equals), 2:Result displayed (faltaria si quiero operar con resultado?)
        int maxDigits = 11;

        // I have divided the code in 2 sections: "Number input" and "Operations and Actions"
        // The number input is from "cell 7" to "cell 9" and to "cell ,"
        // and Operations and Actions is from "cell %" to cell "="

        public MainWindow()
        {
            InitializeComponent();
        }

        // Number input
        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            addNumber(0);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            addNumber(1);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            addNumber(2);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            addNumber(3);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            addNumber(4);
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            addNumber(5);
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            addNumber(6);
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            addNumber(7);
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            addNumber(8);
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            addNumber(9);
        }

        private void Button_Click_plusMinus(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        string operatorText = fieldOperator1.Text;
                        if (operatorText == "") fieldOperator1.Text = "-";
                        else if (operatorText[0] == '-')
                        {
                            fieldOperator1.Text = operatorText.Trim('-');
                        }
                        else fieldOperator1.Text = "-" + fieldOperator1.Text;
                        break;
                    }
                case 1:
                    {
                        string operatorText = fieldOperator2.Text;
                        if (operatorText == "") fieldOperator2.Text = "-";
                        else if (operatorText[0] == '-')
                        {
                            fieldOperator2.Text = operatorText.Trim('-');
                        }
                        else fieldOperator2.Text = "-" + fieldOperator2.Text;
                        break;
                    }

                //case 1: fieldOperator2.Text = "-" + fieldOperator2.Text; break;
                default: MessageBox.Show("You can not add a negative here"); break;
            }
        }

        private void Button_Click_point(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        if (fieldOperator1.Text.IndexOf(',') == -1) fieldOperator1.Text += ",";
                        break;
                    }
                case 1:
                    {
                        if (fieldOperator2.Text.IndexOf(',') == -1) fieldOperator2.Text += ",";
                        break;
                    }
                default: MessageBox.Show("You can not add a decimal here"); break;
            }
        }


        // Operations and Actions

        private void Button_Click_percentage(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        defaultStatus();
                        break;
                    }
                case 1:
                    {
                        if (fieldSymbol.Text == "*" || fieldSymbol.Text == "/")
                        {
                            operator2 = decimal.Parse(fieldOperator2.Text) / 100;
                            fieldOperator2.Text = operator2.ToString();
                        }
                        else if (fieldSymbol.Text == "+" || fieldSymbol.Text == "-")
                        {
                            operator2 = decimal.Parse(fieldOperator2.Text) / 100 * decimal.Parse(fieldOperator1.Text);
                            fieldOperator2.Text = operator2.ToString();
                        }
                        break;
                    }
                case 2:
                    {
                        if (fieldSymbol.Text == "*" || fieldSymbol.Text == "/")
                        {
                            result = decimal.Parse(fieldResult.Text) / 100;
                            fieldResult.Text = result.ToString();
                        }
                        else if (fieldSymbol.Text == "+" || fieldSymbol.Text == "-")
                        {
                            MessageBox.Show("% can't be used here");
                        }
                        break;
                    }
            }
        }

        private void Button_Click_CE(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        fieldOperator1.Text = "";
                        break;
                    }
                case 1:
                    {
                        fieldOperator2.Text = "";
                        break;
                    }
                case 2:
                    {
                        defaultStatus();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("CE ERROR");
                        break;
                    }
            }
        }

        private void Button_Click_C(object sender, RoutedEventArgs e)
        {
            defaultStatus();
        }

        private void Button_Click_deleteLastDigit(object sender, RoutedEventArgs e)
        {
            string numberToDelete;
            switch (currentStatus)
            {
                case 0:
                    {
                        numberToDelete = fieldOperator1.Text;
                        if (numberToDelete.Length > 0) fieldOperator1.Text = numberToDelete.Remove(numberToDelete.Length - 1);
                        break;
                    }
                case 1:
                    {
                        numberToDelete = fieldOperator2.Text;
                        if (numberToDelete.Length > 0) fieldOperator2.Text = numberToDelete.Remove(numberToDelete.Length - 1);
                        break;
                    }
                case 2:
                    {
                        defaultStatus();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("deleteLastDigit ERROR");
                        break;
                    }
            }
        }

        private void Button_Click_fraction(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        if (fieldOperator1.Text == "" || fieldOperator1.Text == "0")
                        {
                            MessageBox.Show("You can not divide by 0, input another number");
                        }
                        else
                        {
                            operator1 = decimal.Parse(fieldOperator1.Text);
                            operator1 = 1 / operator1;
                            maxDigitsOperator1();
                        }
                        break;
                    }
                case 1:
                    {
                        if (fieldOperator2.Text == "" || fieldOperator2.Text == "0")
                        {
                            MessageBox.Show("You can not divide by 0, input another number");
                        }
                        else
                        {
                            operator2 = decimal.Parse(fieldOperator2.Text);
                            operator2 = 1 / operator2;
                            maxDigitsOperator2();
                        }
                        break;
                    }
                case 2:
                    {
                        if (fieldResult.Text == "" || fieldResult.Text == "0")
                        {
                            MessageBox.Show("You can not divide by 0, input another number");
                        }
                        else
                        {
                            result = decimal.Parse(fieldResult.Text);
                            result = 1 / result;
                            fieldResult.Text = result.ToString();
                        }
                        break;
                    }
            }
        }

        private void Button_Click_power2(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        if (fieldOperator1.Text == "")
                        {
                            fieldOperator1.Text="0";
                            break;
                        }
                        double op1= double.Parse(fieldOperator1.Text);
                        operator1 = (decimal) Math.Pow(op1, 2);
                        fieldOperator1.Text = operator1.ToString();
                        maxDigitsOperator1();
                        break;
                    }
                case 1:
                    {
                        if (fieldOperator2.Text == "")
                        {
                            fieldOperator2.Text = "0";
                            break;
                        }
                        double op2 = double.Parse(fieldOperator2.Text);
                        operator2 = (decimal)Math.Pow(op2, 2);
                        fieldOperator2.Text = operator2.ToString();
                        maxDigitsOperator2();
                        break;
                    }
                case 2:
                    {
                        double res = double.Parse(fieldResult.Text);
                        result = (decimal)Math.Pow(res, 2);
                        fieldResult.Text = result.ToString();
                        maxDigitsResult();
                        break;
                    }
            }
        }

        private void Button_Click_root2(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        if (fieldOperator1.Text == "")
                        {
                            fieldOperator1.Text = "0";
                            break;
                        }
                        double op1 = double.Parse(fieldOperator1.Text);
                        operator1 = (decimal)Math.Pow(op1, (0.5));
                        fieldOperator1.Text = operator1.ToString();
                        maxDigitsOperator1();
                        break;
                    }
                case 1:
                    {
                        if (fieldOperator2.Text == "")
                        {
                            fieldOperator2.Text = "0";
                            break;
                        }
                        double op2 = double.Parse(fieldOperator2.Text);
                        operator2 = (decimal)Math.Pow(op2, 0.5);
                        fieldOperator2.Text = operator2.ToString();
                        maxDigitsOperator2();
                        break;
                    }
                case 2:
                    {
                        double res = double.Parse(fieldResult.Text);
                        result = (decimal)Math.Pow(res, 0.5);
                        fieldResult.Text = result.ToString();
                        maxDigitsResult();
                        break;
                    }
            }
        }

        private void Button_Click_division(object sender, RoutedEventArgs e)
        {
            if (currentStatus == 2)
            {
                defaultStatusSaveResult();
                fieldOperator1.Text = result.ToString();
                fieldSymbol.Text = "/";
            }
            if (currentStatus == 0 && fieldOperator1.Text != "")
            {
                currentStatus = 1;
                fieldSymbol.Text = "/";
            }
            else inputError(currentStatus);
        }

        private void Button_Click_multiplication(object sender, RoutedEventArgs e)
        {
            if (currentStatus == 2)
            {
                defaultStatusSaveResult();
                fieldOperator1.Text = result.ToString();
                fieldSymbol.Text = "*";
            }
            if (currentStatus == 0 && fieldOperator1.Text != "")
            {
                currentStatus = 1;
                fieldSymbol.Text = "*";
            }
            else inputError(currentStatus);
        }

        private void Button_Click_subtraction(object sender, RoutedEventArgs e)
        {
            if (currentStatus == 2)
            {
                defaultStatusSaveResult();
                fieldOperator1.Text = result.ToString();
                fieldSymbol.Text = "-";
            }
            if (currentStatus == 0 && fieldOperator1.Text != "")
            {
                currentStatus = 1;
                fieldSymbol.Text = "-";
            }
            else inputError(currentStatus);
        }

        private void Button_Click_addition(object sender, RoutedEventArgs e)
        {
            if (currentStatus == 2)
            {
                defaultStatusSaveResult();
                fieldOperator1.Text = result.ToString();
                fieldSymbol.Text = "+";
            }
            if (currentStatus == 0 && fieldOperator1.Text != "")
            {
                currentStatus = 1;
                fieldSymbol.Text = "+";
            }
            else inputError(currentStatus);
        }

        private void Button_Click_equals(object sender, RoutedEventArgs e)
        {
            if (currentStatus == 1)
            {
                currentStatus = 2;
                // If input is blank, -, ',' or "-," it will get assigned a 0
                if (fieldOperator1.Text == "" || fieldOperator1.Text == "-" || fieldOperator1.Text == "," || fieldOperator1.Text == "-,") fieldOperator1.Text = "0";
                if (fieldOperator2.Text == "" || fieldOperator2.Text == "-" || fieldOperator2.Text == "," || fieldOperator2.Text == "-,") fieldOperator2.Text = "0";

                operator1 = decimal.Parse(fieldOperator1.Text);
                operator2 = decimal.Parse(fieldOperator2.Text);

                switch (fieldSymbol.Text)
                {
                    case "+":
                        {
                            result = operator1 + operator2;
                            break;
                        }
                    case "-":
                        {
                            result = operator1 - operator2;
                            break;
                        }
                    case "*":
                        {
                            result = operator1 * operator2;
                            break;
                        }
                    case "/":
                        {
                            if (operator2 == 0)
                            {
                                MessageBox.Show("You can not divide by 0, input another number");
                                currentStatus = 1;
                                break;
                            }
                            result = operator1 / operator2;
                            break;
                        }
                }
                maxDigitsResult();
            }
            else inputError(currentStatus);
        }


        // Functions used by the program

        private void addNumber(decimal num)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        if (fieldOperator1.Text != "0") fieldOperator1.Text += num.ToString();
                        break;
                    }
                case 1:
                    {
                        if (fieldOperator2.Text != "0") fieldOperator2.Text += num.ToString();
                        if (fieldOperator2.Text == "0" && fieldSymbol.Text == "/" && num != 0) fieldOperator2.Text += num.ToString(); //TODO ERROR HERE (probably)!
                        break;
                    }
                case 2:
                    {
                        currentStatus = 0;
                        defaultStatus();
                        addNumber(num);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("addNumber ERROR");
                        break;
                    }
            }
        }

        // Status before first operation
        private void defaultStatus()
        {
            currentStatus = 0;
            fieldOperator1.Text = fieldOperator2.Text = fieldSymbol.Text = fieldEquals.Text = "";
            fieldResult.Text = "0";
            operator1 = operator2 = result = 0;
        }

        // Same as defaultStatus, but doesn't delete the result
        private void defaultStatusSaveResult()
        {
            currentStatus = 0;
            fieldOperator1.Text = fieldOperator2.Text = fieldSymbol.Text = "";
            fieldResult.Text = "0";
            operator1 = operator2 = 0;
        }

        private void inputError(int currentStatus)
        {
            switch (currentStatus)
            {
                case 0: MessageBox.Show("Current status: 0. Input operator1 and an operation to continue"); break;
                case 1: MessageBox.Show("Current status: 1. Input operator2 and = to continue"); break;
                case 2: MessageBox.Show("Current status: 2. Result displayed, input new number to start a new operation or press a symbol to keep operating same number"); break;
                default: MessageBox.Show("Something went really bad (inputError ERROR)"); break;
            }
        }

        private void maxDigitsOperator1()
        {
            string operator1String = operator1.ToString();
            if (operator1String.Length >= maxDigits) operator1String = operator1String.Remove(maxDigits);
            fieldOperator1.Text = operator1String;
        }

        private void maxDigitsOperator2()
        {
            string operator2String = operator2.ToString();
            if (operator2String.Length >= maxDigits) operator2String = operator2String.Remove(maxDigits);
            fieldOperator2.Text = operator2String;
        }

        private void maxDigitsResult()
        {
            string resultString = result.ToString();
            if (resultString.Length >= maxDigits) resultString = resultString.Remove(maxDigits);
            fieldResult.Text = resultString;

            // Delete the excess of 0s (if they exist) and replace in fieldResult and result
            if (resultString.Length > 1)
            {
                string resultTruncated = resultString;
                resultTruncated = resultTruncated.TrimEnd('0');
                fieldResult.Text = resultTruncated;
                if (resultTruncated != "-") result = decimal.Parse(resultTruncated);
            }
            fieldEquals.Text = "=";

            // Delete the last "," if it was left after a decimal "became" an int in the last operation
            int resultLength = fieldResult.Text.Length;
            resultString = fieldResult.Text;
            if (resultString[resultLength - 1] == ',') fieldResult.Text = resultString.Remove(resultString.Length - 1);
        }
    }
}