﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace webTest
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public  App()
        {
            

        }
        private void OnExit(object sender, ExitEventArgs e)
        {
            webTest.Properties.Settings.Default.Save();

        }
    }
}
