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
        decimal operator1, operator2, result;
        int currentField=1;
        bool allowOperation=true;
        bool equalsPressed,deleteOperator2 = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            checkNumber(0);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            checkNumber(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            checkNumber(2);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            checkNumber(3);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            checkNumber(4);
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            checkNumber(5);
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            checkNumber(6);
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            checkNumber(7);
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            checkNumber(8);
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            checkNumber(9);
        }

        private void Button_Click_addition(object sender, RoutedEventArgs e)
        {
            if (allowOperation)
            {
                fieldSymbol.Text = "+";
                allowOperation = false;
            }
            else if (equalsPressed)
            {
                equalsPressed = false;
                fieldOperator1.Text = fieldResult.Text;
                fieldOperator2.Text = fieldResult.Text = "";
                fieldSymbol.Text = "+";
                fieldEquals.Text = "";
            }
            else
            {
                MessageBox.Show("Presione signo igual = para finalizar la operación actual");
            }
        }

        private void Button_Click_equals(object sender, RoutedEventArgs e)
        {
            operator1 = decimal.Parse(fieldOperator1.Text);
            operator2 = decimal.Parse(fieldOperator2.Text);
            switch (fieldSymbol.Text)
            {
                case "+":
                {
                    result = operator1 + operator2;
                    
                    fieldResult.Text = result.ToString();
                    break;
                }
            }

            // Delete the excess of 0s (if they exist)
            decimal resultTruncated = decimal.Truncate(result);
            if ((result) / resultTruncated == 1)
            {
                fieldResult.Text = resultTruncated.ToString();
            }

            fieldEquals.Text = "=";
            equalsPressed = true;
            deleteOperator2 = true;
        }

        private void Button_Click_C(object sender, RoutedEventArgs e)
        {
            fieldOperator1.Text = fieldOperator2.Text = fieldSymbol.Text = "";
            fieldResult.Text = "0";
            operator1 = operator2 = 0;
            allowOperation = true;
        }

        private void Button_Click_point(object sender, RoutedEventArgs e)
        {
            if (allowOperation) fieldOperator1.Text += ",";
            else fieldOperator2.Text += ",";
        }

        private void Button_Click_deleteLastDigit(object sender, RoutedEventArgs e)
        {
            string delete;

            delete = fieldResult.Text;
            if (delete.Length>0) fieldResult.Text = delete.Remove(delete.Length-1);
            if (fieldResult.Text == "") fieldResult.Text = "0";




            //TODO Permitir que el delete elimine digitos del op1 y op2
        }

        private void checkNumber(decimal num)
        {
            if (fieldOperator1.Text == "" || allowOperation) addOperator1(num);
            else addOperator2(num);
        }

        private void addOperator1(decimal num)
        {
            fieldOperator1.Text += num.ToString();
        }
        private void addOperator2(decimal num)
        {
            if (deleteOperator2)
            {
                fieldOperator2.Text = "";
                deleteOperator2 = false;
                fieldOperator2.Text += num.ToString();
            }
            else
            {
                fieldOperator2.Text += num.ToString();
            }
        }

    }
}