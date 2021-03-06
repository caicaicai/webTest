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
using webTest.Model;
using webTest.ViewModel;

namespace webTest.View
{
    /// <summary>
    /// OptionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OptionWindow : Window
    {
        public OptionWindow(Option option)
        {
            InitializeComponent();
            DataContext = new OptionViewModel(option);
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox depth = (TextBox)sender;
            int value;
            if(!int.TryParse(depth.Text, out value))
            {
                MessageBox.Show("Depth only accept integer!");
                depth.Text = "1";
            }
        }
    }
}
