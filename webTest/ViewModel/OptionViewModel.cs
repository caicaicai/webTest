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

        public OptionViewModel(Option option)
        {
            this.option = option;
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
