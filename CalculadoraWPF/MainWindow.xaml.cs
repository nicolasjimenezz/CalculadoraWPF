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
        decimal operator1=0, operator2=0, result=0;
        int currentStatus = 0; // 0:Input operator1 +(user press symbol), 1:Waiting for input operator2 +(user press equals), 2:Result displayed (faltaria si quiero operar con resultado?)
        int maxDigits = 11;

        public MainWindow()
        {
            InitializeComponent();
        }
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

        private void Button_Click_addition(object sender, RoutedEventArgs e)
        {
            if (currentStatus == 2)
            {
                defaultStatusSaveResult();
                fieldOperator1.Text = result.ToString();
                fieldSymbol.Text = "+";
            }
            if (currentStatus == 0 && fieldOperator1.Text!="")
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
                string resultString = result.ToString();
                if (resultString.Length >= maxDigits) resultString = resultString.Remove(maxDigits);
                fieldResult.Text = resultString;

                // Delete the excess of 0s (if they exist) and replace in fieldResult and result
                if (resultString.Length > 1)
                {
                    string resultTruncated = resultString;
                    resultTruncated=resultTruncated.TrimEnd('0');
                    fieldResult.Text = resultTruncated;
                    if (resultTruncated != "-") result = decimal.Parse(resultTruncated);
                }
                fieldEquals.Text = "=";

                // Delete the last "," if it was left after a decimal "became" an int in the last operation
                int resultLength = fieldResult.Text.Length;
                resultString = fieldResult.Text;
                if (resultString[resultLength - 1] == ',') fieldResult.Text= resultString.Remove(resultString.Length - 1);
            }
            else inputError(currentStatus);
        }

        private void Button_Click_CE(object sender, RoutedEventArgs e)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        fieldOperator1.Text="";
                        break;
                    }
                case 1:
                    {
                        fieldOperator2.Text="";
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

        private void Button_Click_deleteLastDigit(object sender, RoutedEventArgs e)
        {
            string numberToDelete;
            switch (currentStatus)
            {
                case 0:
                    {
                        numberToDelete = fieldOperator1.Text;
                        if (numberToDelete.Length > 0) fieldOperator1.Text=numberToDelete.Remove(numberToDelete.Length - 1);
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

        private void addNumber(decimal num)
        {
            switch (currentStatus)
            {
                case 0:
                    {
                        if (fieldOperator1.Text!="0") fieldOperator1.Text += num.ToString();
                        break;
                    }
                case 1:
                    {
                        if (fieldOperator2.Text != "0") fieldOperator2.Text += num.ToString();
                        if (fieldOperator2.Text == "0" && fieldSymbol.Text=="/" && num!=0) fieldOperator2.Text += num.ToString(); //TODO ERROR HERE (probably)!
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

        private void defaultStatus() // Status before first operation
        {
            currentStatus = 0;
            fieldOperator1.Text = fieldOperator2.Text = fieldSymbol.Text = fieldEquals.Text = "";
            fieldResult.Text = "0";
            operator1 = operator2 = result = 0;
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
    }
}