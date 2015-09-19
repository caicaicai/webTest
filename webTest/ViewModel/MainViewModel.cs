using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using webTest.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace webTest.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ICommand ShowPopUp { get; private set; }
        public ICommand DeleteItem { get; private set; }
        public ICommand Save { get; private set; }
        public ICommand Open { get; private set; }
        public ICommand JsonView { get; private set; }
        public ICommand TabSelectionChanged { get; private set; }
        private BackgroundWorker backgroundWorker;

        private ObservableCollection<TabItem> _tabItems;
        private int _selectedTabIndex;


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            ShowPopUp = new RelayCommand(() => ShowPopUpExecute(), () => { 
                if(TabItems.Count == 0 )
                {
                    return false;
                }
                return TabItems[SelectedTabIndex].IsRequesting == false; 
            });
            DeleteItem = new RelayCommand(() => DeleteItemExecute(), () => { return SelectedTabIndex != TabItems.Count - 1; });
            Save = new RelayCommand(() => SaveExecute(), () => {return true;});
            Open = new RelayCommand(() => OpenExecute(), () => { return true; });
            JsonView = new RelayCommand(() => JsonViewExecute(), () => { return true; });
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            SelectedTabIndex = 0;

            TabItems = new ObservableCollection<TabItem>();
            TabItems.Add(new TabItem());


            // Registers for incoming Notification messages.
            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                // Checks the actual content of the message.
                switch (message.Notification)
                {
                    case "GotoDetailsPage":
                        break;

                    case "OtherMessage":
                        break;
                    default:
                        break;
                }
            });
 
        }

        private void ShowPopUpExecute()
        {
            // Sends a notification message with a string content.
            Messenger.Default.Send(new NotificationMessage("GotoDetailsPage"));


            if (SelectedTabIndex == TabItems.Count - 1)
            {
                TabItems.Add(new TabItem());
            }
            Requester rq = new Requester(TabItems[SelectedTabIndex]);
            try
            {
                TabItems[SelectedTabIndex].IsRequesting = true;
                backgroundWorker.RunWorkerAsync(rq);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                Console.WriteLine(e);
            }
        }

        private void DeleteItemExecute()
        {

            if (SelectedTabIndex == TabItems.Count - 1)
            {
                //Of course, this statement will never be executed
                MessageBox.Show("不能移除未保存的项目！");
            }

            try
            {
                SelectedTabIndex++;
                TabItems.RemoveAt(SelectedTabIndex - 1);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SaveExecute()
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "config"; // Default file name
            dlg.DefaultExt = ".osl"; // Default file extension
            dlg.Filter = "Text documents (.osl)|*.osl"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save Config 
                Config.Save(dlg.FileName, new Config(TabItems));
            }
        }

        private void OpenExecute()
        {
            // Configure save file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "config"; // Default file name
            dlg.DefaultExt = ".osl"; // Default file extension
            dlg.Filter = "Text documents (.osl)|*.osl"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                SelectedTabIndex = 0;
                // Load Config 
                Config cfg = Config.Load(dlg.FileName);
                TabItems = cfg.TabItems;
            }
        }

        private void JsonViewExecute()
        {
            Console.WriteLine("JsonViewExecute...");
            Messenger.Default.Send(new NotificationMessage<string>(CurrentItem.ResponseContent, "ShowJsonView"));
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Requester rq = (Requester)e.Argument;
                // Return the value through the Result property.
                //e.Result = rq.DoRequest();
                rq.DoRequest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TabItems[SelectedTabIndex].ResponseContent = (string)e.Result;
            if (TabItems[SelectedTabIndex].Title.Length == 0)
            {
                TabItems[SelectedTabIndex].Title = TabItems[SelectedTabIndex].RequestUrl.Substring(0, 6);
            }
            TabItems[SelectedTabIndex].IsRequesting = false;
        }

        public string RequestBtn
        {
            get
            {
                if (TabItems[SelectedTabIndex].IsRequesting == true)
                {
                    return "请求中..";
                }
                else
                {
                    return "请求";
                }
            }
        }

        public ObservableCollection<TabItem> TabItems
        {
            get
            {
                return _tabItems;
            }
            set
            {
                _tabItems = value;
                RaisePropertyChanged("TabItems");
            }
        }

        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged("SelectedTabIndex");
                RaisePropertyChanged("CurrentItem");
            }
        }

        public TabItem CurrentItem
        {
            get
            {
                if(TabItems.Count > 0)
                {
                    if(SelectedTabIndex < 0)
                    {
                        return TabItems[0];
                    }
                    return TabItems[SelectedTabIndex];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (TabItems[SelectedTabIndex] == value)
                    return;

                TabItems[SelectedTabIndex] = value;

                RaisePropertyChanged("CurrentItem");
            }
        }

    }

}