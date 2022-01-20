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
using Wpf_1_2_final.Models;

namespace Wpf_1_2_final
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            display();
        }
        private string[] nums = new string[9];
        private int numCount = 0;
        private StringBuilder action = new StringBuilder();
        private StringBuilder inputnum = new StringBuilder();
        //Математические операции
        private void aryOp(object sender, RoutedEventArgs e)
        {
            action.Append((sender as Button).Content.ToString());
            inputnum.Clear();

            numCount++;
            display();
        }
        //Выбор значения
        private void addValue(object sender, RoutedEventArgs e)
        {
            inputnum.Append((sender as Button).Content.ToString());
            updateNum();

            display();
        }
        //Расчёты
        #region Расчётная часть
        private void Result(object sender, RoutedEventArgs e)
        {
            double result = 0, count = 0;
            int divcount = 0, mulcount = 0, addcount = 0, mincount = 0;
            StringBuilder mas = new StringBuilder();
            List<char> input = new List<char>();
            foreach (char item in action.ToString())
            {
                input.Add(item);
            }
            //Сортировка в массив
            while (count < input.Count)
            {
                //если содержит символ, то произвести сортировку
                if (input.Contains('/'))
                {
                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i].ToString() == "/")
                        {
                            mas.Append(i.ToString() + i.ToString() + "/" + (i + 1));
                            count++;
                            divcount++;
                        }
                    }
                }
                if (input.Contains('*'))
                {
                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i].ToString() == "*")
                        {
                            mas.Append(i.ToString() + i.ToString() + "*" + (i + 1));
                            count++;
                            mulcount++;
                        }
                    }
                }
                if (input.Contains('+'))
                {
                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i].ToString() == "+")
                        {
                            mas.Append(i.ToString() + i.ToString() + "+" + (i + 1));
                            count++;
                            addcount++;
                        }
                    }
                }
                if (input.Contains('-'))
                {
                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i].ToString() == "-")
                        {
                            mas.Append(i.ToString() + i.ToString() + "-" + (i + 1));
                            count++;
                            mincount++;
                        }
                    }
                }
            }
            bool calculation = false;
            if (divcount > 0)
            {
                for (int j = 0; j < mas.Length; j += 4)
                {
                    if (mas[j + 2].ToString() == "/")
                    {
                        int num1 = mas[j + 1] - 48;
                        int num2 = mas[j + 3] - 48;

                        result += Ariph.Divide(Convert.ToDouble(nums[num1]), Convert.ToDouble(nums[num2]));
                        calculation = true;
                    }
                }
            }
            if (mulcount > 0)
            {
                for (int j = 0; j < mas.Length; j += 4)
                {
                    if (mas[j + 2].ToString() == "*" && calculation == false)
                    {
                        int num1 = mas[j + 1] - 48;
                        int num2 = mas[j + 3] - 48;

                        result += Ariph.Multiply(Convert.ToInt32(nums[num1]), Convert.ToInt32(nums[num2]));
                        calculation = true;
                    }
                    else if (mas[j + 2].ToString() == "*" && calculation == true)
                    {
                        int num1 = (mas[j - 2].ToString() == "*") ? mas[j + 3] - 48 : mas[j + 1] - 48;
                        int num2 = (int)result;

                        result += Ariph.Multiply(Convert.ToInt32(nums[num1]), num2);
                        calculation = true;
                    }
                }
            }
            if (mincount > 0)
            {
                for (int j = 0; j < mas.Length; j += 4)
                {
                    if (mas[j + 2].ToString() == "-" && calculation == false)
                    {
                        int num1 = mas[j + 1] - 48;
                        int num2 = mas[j + 3] - 48;

                        result += Ariph.Subtract(Convert.ToInt32(nums[num1]), Convert.ToInt32(nums[num2]));
                    }
                    else if (mas[j + 2].ToString() == "-" && calculation == true)
                    {
                        int num1 = (mas[j - 2].ToString() == "-") ? mas[j + 3] - 48 : mas[j + 1] - 48;
                        int num2 = (int)result;
                        result = Ariph.Subtract(num2, Convert.ToInt32(nums[num1]));

                        calculation = true;
                    }
                }
            }
            if (addcount > 0)
            {
                for (int j = 0; j < mas.Length; j += 4)
                {
                    if (mas[j + 2].ToString() == "+" && calculation == false)
                    {
                        int num1 = mas[j + 1] - 48;
                        int num2 = mas[j + 3] - 48;

                        result += Ariph.Add(Convert.ToInt32(nums[num1]), Convert.ToInt32(nums[num2]));
                        calculation = true;
                    }
                    else if (mas[j + 2].ToString() == "+" && calculation == true)
                    {
                        int num1 = (mas[j - 2].ToString() == "+") ? mas[j + 3] - 48 : mas[j + 1] - 48;
                        int num2 = (int)result;

                        result = Ariph.Add(Convert.ToInt32(nums[num1]), num2);
                        calculation = true;
                    }
                }
            }
            Clear();
            nums[0] = result.ToString();
            Display.Content = result;
            action.Clear();
        }
        #endregion
        //Очистка
        #region Очистка
        private void Clear(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            inputnum.Clear();
            numCount = 0;

            for (int i = 1; i < 9; i++)
            {
                nums[i] = null;
            }

            action.Clear();
            Display.Content = "0";
        }
        #endregion
        //Вывод на дисплей
        #region Вывод на дисплей
        private void display()
        {
            StringBuilder displayOut = new StringBuilder();
            int counter = 0;

            for (int i = 0; i < 9; i++)
            {
                if (nums[i] != null)
                {
                    counter++;
                }
            }
            if (nums[0] == null)
            {
                displayOut.Append("0");
            }
            else if (nums[0] != null && action.ToString() != "")
            {

                for (int i = 0; i < counter; i++)
                {
                    displayOut.Append(nums[i] + " ");

                    if (i < action.Length)
                    {
                        displayOut.Append(action[i] + " ");
                    }

                }

            }
            else if (nums[0] != null)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] != null)
                    {
                        displayOut.Append(nums[i] + " ");
                    }
                }
            }

            Display.Content = displayOut.ToString();
        }
        private void updateNum()
        {
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (nums[0] == null)
                {
                    nums[0] = inputnum.ToString();
                    break;
                }
                else if (nums[i] != null)
                {
                    nums[numCount] = inputnum.ToString();
                    break;
                }
            }
        }
        #endregion
    }
}
