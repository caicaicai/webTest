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
                    string filePathFormMainArgs = arguments[1];
                    webTest.Properties.Settings.Default.configPath = filePathFormMainArgs;
                }
            }
            else
            {
                if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null 
                    &&  AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Length > 0)
                    {
                        string fname = String.Empty;
                        try
                        {
                            fname = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
            
                            // It comes in as a URI; this helps to convert it to a path.
                            Uri uri = new Uri(fname);
                            fname = uri.LocalPath;
                            if (fname.EndsWith(".osl"))
                            {
                                webTest.Properties.Settings.Default.configPath = fname;
                            }
  
                        }
                        catch (Exception ex)
                        {
                            // For some reason, this couldn't be read as a URI.
                            // Do what you must...
                            System.Windows.MessageBox.Show(ex.Message);

                        }
                    }
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
