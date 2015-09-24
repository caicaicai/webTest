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
using System.Linq;

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
        public ICommand OpenOption { get; private set; }
        public ICommand About { get; private set; }


        public ICommand SpecialView { get; private set; }
        public ICommand TabSelectionChanged { get; private set; }
        private BackgroundWorker backgroundWorker;

        
        private Option _option;
        //private ObservableCollection<TabItem> _tabItems;
        private int _selectedTabIndex;
        private Dictionary<string, ObservableCollection<TabItem>> _tabItemsGroup;
        private string _selectedGroupIndex;

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
                return CurrentItem.IsRequesting == false; 
            });
            DeleteItem = new RelayCommand(() => DeleteItemExecute(), () => { return SelectedTabIndex != CurrentTabItems.Count - 1; });
            Save = new RelayCommand(() => SaveExecute(), () => {return true;});
            Open = new RelayCommand(() => OpenExecute(), () => { return true; });
            OpenOption = new RelayCommand(() => { Messenger.Default.Send(new NotificationMessage<Option>(option, "option")); });
            About = new RelayCommand(() => { Messenger.Default.Send(new NotificationMessage<string>("PlaceHolder","About")); });
            SpecialView = new RelayCommand<object>((param) => SpecialViewExecute(param), (param) => { return true; });
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            TabItemsGroup = new Dictionary<string, ObservableCollection<TabItem>> { { "default", new ObservableCollection<TabItem> { new TabItem() } } };
            SelectedGroupIndex = TabItemsGroup.First().Key;
            SelectedTabIndex = 0;
            option = new Option();

        }


        private void ShowPopUpExecute()
        {
            // Sends a notification message with a string content.
            Messenger.Default.Send(new NotificationMessage("GotoDetailsPage"));

            if (SelectedTabIndex == CurrentTabItems.Count - 1)
            {
                CurrentTabItems.Add(new TabItem());
            }
            Requester rq = new Requester(CurrentTabItems[SelectedTabIndex]);
            try
            {
                CurrentTabItems[SelectedTabIndex].IsRequesting = true;
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

            if (SelectedTabIndex == CurrentTabItems.Count - 1)
            {
                //Of course, this statement will never be executed
                MessageBox.Show("不能移除未保存的项目！");
            }

            try
            {
                SelectedTabIndex++;
                CurrentTabItems.RemoveAt(SelectedTabIndex - 1);

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
                Config.Save(dlg.FileName, new Config(TabItemsGroup, option));
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
                TabItemsGroup = cfg.TabItemsGroup;
            }
        }

        private void SpecialViewExecute(object obj)
        {
            if (CurrentItem.ResponseContent == null || CurrentItem.ResponseContent.Length == 0)
            {
                MessageBox.Show("Stuff Needed.");
                return;
            }

            Messenger.Default.Send(new NotificationMessage<string>(CurrentItem.ResponseContent, obj.ToString()));
            
            
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
            if (CurrentItem.Title.Length == 0)
            {
                CurrentItem.Title = CurrentItem.RequestUrl.Substring(0, 6);
            }
            CurrentItem.IsRequesting = false;
        }

        public string RequestBtn
        {
            get
            {
                if (CurrentItem.IsRequesting == true)
                {
                    return "请求中..";
                }
                else
                {
                    return "请求";
                }
            }
        }

        public Dictionary<string, ObservableCollection<TabItem>> TabItemsGroup
        {
            get
            {
                return _tabItemsGroup;
            }
            set
            {
                if (_tabItemsGroup == value)
                    return;

                _tabItemsGroup = value;
                RaisePropertyChanged("TabItemsGroup");
            }
        }
        public string SelectedGroupIndex
        {
            get
            {
                return _selectedGroupIndex;
            }
            set
            {
                _selectedGroupIndex = value;
                RaisePropertyChanged("SelectedGroupIndex");
                RaisePropertyChanged("CurrentItem");
            }
        }

        public ObservableCollection<TabItem> CurrentTabItems
        {
            get
            {
                return TabItemsGroup[SelectedGroupIndex];
            }
            set
            {
                if (TabItemsGroup[SelectedGroupIndex] == value)
                    return;

                TabItemsGroup[SelectedGroupIndex] = value;

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
                if (CurrentTabItems.Count > 0)
                {
                    if (SelectedTabIndex < 0)
                    {
                        return CurrentTabItems[0];
                    }
                    return CurrentTabItems[SelectedTabIndex];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (CurrentTabItems[SelectedTabIndex] == value)
                    return;

                CurrentTabItems[SelectedTabIndex] = value;
                RaisePropertyChanged("CurrentItem");
            }
        }

        public Option option
        {
            get
            {
                return _option;
            }
            set
            {
                if (_option == value)
                    return;

                _option = value;

                RaisePropertyChanged("option");
            }
        }

    }

}