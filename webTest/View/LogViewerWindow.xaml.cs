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
using System.Windows.Shapes;
using webTest.ViewModel;
using webTest.Model;

namespace webTest.View
{
    /// <summary>
    /// LogViewerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogViewerWindow : Window
    {
        public LogViewerWindow(Logger logger)
        {
            InitializeComponent();
            DataContext = new LogViewerModel(logger);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyScrollViewer.ScrollToBottom();
        }

    }
}
