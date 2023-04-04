using Newtonsoft.Json;
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

namespace Практическая_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Zapis> zapisi;

        public MainWindow()
        {
            InitializeComponent();

            zapisi = Serializer.Load<List<Zapis>>("zapisi.json");
            if (zapisi == null)
            {
                zapisi = new List<Zapis>();
            }

            DateSelect.Text = DateTime.Now.ToString();
            UpdateZapisi();
            UpdateTypes();
        }

        private void UpdateZapisi()
        {
            string date = DateSelect.Text;
            List<Zapis> finded = zapisi.FindAll(item => item.date == date);
            ZapisiDataGrid.ItemsSource = null;
            ZapisiDataGrid.ItemsSource = finded;

            double total = 0;
            foreach (var zapis in finded)
            {
                if (zapis.income)
                {
                    total += zapis.Sum;
                }
                else
                {
                    total -= zapis.Sum;
                }
            }
            TotalLabel.Content = $"Итого: {total}";
        }

        private void UpdateTypes()
        {
            string item = TypeComboBox.SelectedItem as string;
            List<string> types = Serializer.Load<List<string>>("types.json");
            TypeComboBox.ItemsSource = types;
            TypeComboBox.SelectedItem = item;
        }

        private void AddZapis(string date, string name, string typeName, double sum)
        {
            Zapis zapis = new Zapis(date, name, typeName, sum);
            zapisi.Add(zapis);
            Serializer.Save<List<Zapis>>("zapisi.json", zapisi);
            UpdateZapisi();
        }

        private void DeleteZapis(Zapis zapis)
        {
            zapisi.Remove(zapis);
            Serializer.Save<List<Zapis>>("zapisi.json", zapisi);
            UpdateZapisi();
        }

        private void ChangeZapis(Zapis zapis, string name, string typeName, double sum)
        {
            zapis.name = name;
            zapis.typeName = typeName;
            zapis.Sum = sum;
            Serializer.Save<List<Zapis>>("zapisi.json", zapisi);
            UpdateZapisi();
        }

        private void AddTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddTypeWindow window = new AddTypeWindow();
            window.ShowDialog();
            UpdateTypes();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string date = DateSelect.Text;
            string name = NameTextBox.Text;
            string typeName = TypeComboBox.SelectedItem as string;
            string sumStr = SumTextBox.Text;

            if (date == "" || name == "" || typeName == null || sumStr == "")
            {
                return;
            }

            double sum;
            try
            {
                sum = Convert.ToDouble(sumStr);
            }
            catch
            {
                MessageBox.Show("Сумма должна быть числом");
                return;
            }

            AddZapis(date, name, typeName, sum);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Zapis zapis = ZapisiDataGrid.SelectedItem as Zapis;
            if (zapis == null)
            {
                return;
            }

            DeleteZapis(zapis);
        }

        private void DateSelect_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateZapisi();
        }

        private void ZapisiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Zapis zapis = ZapisiDataGrid.SelectedItem as Zapis;
            if (zapis == null)
            {
                NameTextBox.Text = "";
                SumTextBox.Text = "";
                TypeComboBox.SelectedItem = null;
                return;
            }
            
            NameTextBox.Text = zapis.name;
            TypeComboBox.SelectedItem = zapis.typeName;
            string sumStr = zapis.Sum.ToString();
            if (!zapis.income)
            {
                sumStr = "-" + sumStr;
            }
            SumTextBox.Text = sumStr;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            Zapis zapis = ZapisiDataGrid.SelectedItem as Zapis;
            if (zapis == null)
            {
                return;
            }

            string name = NameTextBox.Text;
            string typeName = TypeComboBox.SelectedItem as string;
            string sumStr = SumTextBox.Text;
            if (name == "" || typeName == null || sumStr == "")
            {
                return;
            }

            double sum;
            try
            {
                sum = Convert.ToDouble(sumStr);
            }
            catch
            {
                MessageBox.Show("Сумма должна быть числом");
                return;
            }

            ChangeZapis(zapis, name, typeName, sum);
        }
    }
}
