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
using System.Collections.Generic;
using webTest.Model;
using webTest.View;

namespace webTest.ViewModel
{
    public class OptionViewModel : ViewModelBase
    {
        private Option _option;

        public ICommand AddProxy { get; private set; }
        public ICommand RemoveProxy { get; private set; }

        private string selectedUserAgent;

        public OptionViewModel(Option option)
        {
            this._option = option;

            AddProxy = new RelayCommand(() => AddProxyExec(), () => { return true; });
            RemoveProxy = new RelayCommand(() => RemoveProxyExec(), () => { return true; });
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

        public string SelectedUserAgent
        {
            get
            {
                return selectedUserAgent;
            }
            set
            {
                selectedUserAgent = value;
                RaisePropertyChanged("SelectedUserAgent");
                option.UserAgent = value;
                option.UseUserAgent = true;
            }
        }

        private void AddProxyExec()
        {
            var dialog = new MyDialog();
            if (dialog.ShowDialog() == true)
            {
                option.Proxys.Add(new ProxyServer(dialog.ResponseText, true));
            }
        }

        private void RemoveProxyExec()
        {
            if (option.Proxys.Count > 0)
            {
                option.Proxys.RemoveAt(option.SelectedProxyIndex);
            }
        }


    }
}
