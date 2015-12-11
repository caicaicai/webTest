using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using webTest.Model;
using Xceed.Wpf.Toolkit;

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
            String[] arguments = Environment.GetCommandLineArgs();

            if (arguments.GetLength(0) > 1)
            {
                if (arguments[1].EndsWith(".osl"))
                {
                    System.Windows.MessageBox.Show(arguments[1]);
                    string filePathFormMainArgs = arguments[1];
                    webTest.Properties.Settings.Default.configPath = filePathFormMainArgs;
                    //if (isFileMagiValid(filePathFormMainArgs))
                    //{
                    //    // Step 1 : deserialize filePathFormMainArgs
                    //    // Step 2 : call the view "File oepn" in the application"
                    //}
                }
            }
            else
            {
                // Call the view "welcome page application"
            }
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<string>>(this, NotificationMessageReceived);
            Messenger.Default.Register<NotificationMessage<Option>>(this, NotificationMessageReceived);
            Messenger.Default.Register<NotificationMessage<Logger>>(this, NotificationMessageReceived);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
        }

        private void NotificationMessageReceived(NotificationMessage<string> msg)
        {
            
            if (msg.Notification == "About")
            {
                var aboutView = new AboutBox();
                aboutView.Show();
            }
            if (msg.Notification == "JSON")
            {
                var jsonView = new JsonViewForm(msg.Content);
                jsonView.Show();
            }
            if (msg.Notification == "XML")
            {
                var XMLView = new XMLViewerWindow(msg.Content);
                XMLView.Show();
            }
            if (msg.Notification == "HTML")
            {
                var HTMLView = new HTMLViewWindow(msg.Content);
                HTMLView.Show();
            }
        }
        private void NotificationMessageReceived(NotificationMessage<Option> msg)
        {
            if (msg.Notification == "option")
            {
                var optionView = new OptionWindow(msg.Content);
                optionView.ShowInTaskbar = false;
                optionView.Owner = this;
                optionView.ShowDialog();
            }

        }

        private void NotificationMessageReceived(NotificationMessage<Logger> msg)
        {
            if (msg.Notification == "logviewer")
            {
                var optionView = new LogViewerWindow(msg.Content);
                //optionView.ShowInTaskbar = false;
                //optionView.Owner = this;
                optionView.Show();
            }

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
