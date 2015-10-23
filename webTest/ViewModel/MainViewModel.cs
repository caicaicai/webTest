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
        public ICommand Save { get; private set; }
        public ICommand Open { get; private set; }
        public ICommand OpenOption { get; private set; }
        public ICommand About { get; private set; }

        public ICommand ShowPopUp { get; private set; }
        public ICommand DeleteGroup { get; private set; }
        public ICommand AddGroup { get; private set; }
        public ICommand TabItemsGroupRename { get; private set; }

        public ICommand DeleteTab { get; private set; }


        public ICommand SpecialView { get; private set; }
        public ICommand TabSelectionChanged { get; private set; }
        private BackgroundWorker backgroundWorker;

        public ICommand SaveGroupName { get; private set; }


        private Option _option;

        private ObservableCollection<TabItemsGroup> _tabItemsGroup;
        private int _selectedGroupIndex;

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

            ShowPopUp = new RelayCommand(() => ShowPopUpExecute(), () => { return true; });
            DeleteGroup = new RelayCommand(() => DeleteGroupExecute(), () => { return SelectedGroupIndex != TabItemsGroup.Count - 1; });
            DeleteTab = new RelayCommand(() => DeleteTabExecute(), () => { return CurrentTabItemsGroup.SelectedTabIndex != CurrentTabItemsGroup.TabItems.Count - 1; });
            AddGroup = new RelayCommand(() => AddGroupExecute(), () => { return true; });
            TabItemsGroupRename = new RelayCommand<object>((param) => TabItemsGroupRenameExecute(param), (param) => { return true; });
            Save = new RelayCommand(() => SaveExecute(), () => {return true;});
            Open = new RelayCommand(() => OpenExecute(), () => { return true; });
            OpenOption = new RelayCommand(() => { Messenger.Default.Send(new NotificationMessage<Option>(option, "option")); });
            About = new RelayCommand(() => { Messenger.Default.Send(new NotificationMessage<string>("PlaceHolder","About")); });
            SpecialView = new RelayCommand<object>((param) => SpecialViewExecute(param), (param) => { return true; });
            SaveGroupName = new RelayCommand(() => SaveGroupNameExecute(), () => { return true; });
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            TabItemsGroup = new ObservableCollection<TabItemsGroup> { };
            TabItemsGroup.Add(new TabItemsGroup());
            SelectedGroupIndex = 0;
            option = new Option();

        }

        #region Exec Stuff
        private void ShowPopUpExecute()
        {
            // Sends a notification message with a string content.
            Messenger.Default.Send(new NotificationMessage("GotoDetailsPage"));

            if (CurrentTabItemsGroup.SelectedTabIndex == CurrentTabItemsGroup.TabItems.Count - 1)
            {
                CurrentTabItemsGroup.NewTab();
            }
            Requester rq = new Requester(CurrentTabItemsGroup.CurrentItem, option);
            try
            {
                CurrentTabItemsGroup.CurrentItem.IsRequesting = true;
                backgroundWorker.RunWorkerAsync(rq);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                Console.WriteLine(e);
            }
        }

        private void DeleteGroupExecute()
        {

            if (SelectedGroupIndex == TabItemsGroup.Count - 1)
            {
                //Of course, this statement will never be executed
                MessageBox.Show("不能移除默认组！");
            }

            try
            {
                SelectedGroupIndex++;
                TabItemsGroup.RemoveAt(SelectedGroupIndex - 1);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteTabExecute()
        {

            if (CurrentTabItemsGroup.SelectedTabIndex == CurrentTabItemsGroup.TabItems.Count - 1)
            {
                //Of course, this statement will never be executed
                MessageBox.Show("至少保留一个标签！");
                return;
            }

            try
            {
                
                CurrentTabItemsGroup.TabItems.RemoveAt(CurrentTabItemsGroup.SelectedTabIndex);
                SelectedGroupIndex = 0;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddGroupExecute()
        {
            TabItemsGroup.Add(new TabItemsGroup());
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
                // Load Config 
                Config cfg = Config.Load(dlg.FileName);
                TabItemsGroup = cfg.TabItemsGroup;
                SelectedGroupIndex = 0;
            }
        }

        private void SpecialViewExecute(object obj)
        {
            if (CurrentTabItemsGroup.CurrentItem.ResponseContent == null || CurrentTabItemsGroup.CurrentItem.ResponseContent.Length == 0)
            {
                MessageBox.Show("Stuff Needed.");
                return;
            }

            Messenger.Default.Send(new NotificationMessage<string>(CurrentTabItemsGroup.CurrentItem.ResponseContent, obj.ToString()));


        }

        private void TabItemsGroupRenameExecute(object obj)
        {
            //Visibility vb = (Visibility)obj;
            //vb = Visibility.Visible;
            SaveGroupNameExecute();
            CurrentTabItemsGroup.IsEditing = Visibility.Visible;
        }

        private void SaveGroupNameExecute()
        {
            foreach(TabItemsGroup tig in TabItemsGroup)
            {
                tig.IsEditing = Visibility.Hidden;
            }
        }

        #endregion

        #region BackgroundWorker
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
            if (CurrentTabItemsGroup.CurrentItem.Title.Length == 0)
            {
                CurrentTabItemsGroup.CurrentItem.Title = CurrentTabItemsGroup.CurrentItem.RequestUrl.Substring(0, 6);
            }
            CurrentTabItemsGroup.CurrentItem.IsRequesting = false;
        }
        #endregion
        
        public ObservableCollection<TabItemsGroup> TabItemsGroup
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

        public int SelectedGroupIndex
        {
            get
            {
                if (_selectedGroupIndex < 0)
                    return 0;
                return _selectedGroupIndex;
            }
            set
            {
                if (value < 0)
                    return;
                _selectedGroupIndex = value;
                RaisePropertyChanged("SelectedGroupIndex");
                RaisePropertyChanged("CurrentTabItemsGroup");
            }
        }

        public TabItemsGroup CurrentTabItemsGroup
        {
            get
            {
                if (TabItemsGroup.Count == 0)
                    return null;
                return TabItemsGroup[SelectedGroupIndex];
            }
            set
            {
                if (value == TabItemsGroup[SelectedGroupIndex])
                    return;

                TabItemsGroup[SelectedGroupIndex] = value;
                RaisePropertyChanged("CurrentTabItemsGroup");
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