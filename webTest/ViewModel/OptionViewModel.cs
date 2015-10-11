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

namespace webTest.ViewModel
{
    public class OptionViewModel : ViewModelBase
    {
        private Option _option;

        private string selectedUserAgent;


        public OptionViewModel(Option option)
        {
            this._option = option;
            Random rand = new Random();
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


    }
}
