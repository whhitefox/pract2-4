﻿using System;
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
using System.Windows.Shapes;

namespace Практическая_4
{
    /// <summary>
    /// Логика взаимодействия для AddTypeWindow.xaml
    /// </summary>
    public partial class AddTypeWindow : Window
    {
        public AddTypeWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            if (name == "")
            {
                return;
            }

            List<string> types = Serializer.Load<List<string>>("types.json");
            if (types == null)
            {
                types = new List<string>();
            }
            types.Add(name);
            Serializer.Save("types.json", types);
            Close();
        }
    }
}
