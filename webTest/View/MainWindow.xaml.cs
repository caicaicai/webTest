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
using System.Net;
using System.IO;
using System.Threading;
using GalaSoft.MvvmLight.Messaging;

namespace webTest.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.ComponentModel.BackgroundWorker BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<string>>(this, NotificationMessageReceived);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
        }

        private void NotificationMessageReceived(NotificationMessage<string> msg)
        {
            if (msg.Notification == "ShowJsonView")
            {
                var jsonView = new JsonViewForm(msg.Content);
                jsonView.Show();
            }
            if (msg.Notification == "ShowXMLView")
            {
                var jsonView = new XMLViewerWindow(msg.Content);
                jsonView.Show();
            }
        }


    }
}
