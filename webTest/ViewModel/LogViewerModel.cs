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
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace webTest.ViewModel
{
    public class LogViewerModel : ViewModelBase
    {
        public ICommand Clear { get; private set; }
        public LogViewerModel(Logger lg)
        {
            this.logger = lg;
            Clear = new RelayCommand(() => ClearExecute(), () => { return true; });
        }


        private Logger _logger;

        public Logger logger
        {
            get { return _logger; }
            set {
                _logger = value;
                RaisePropertyChanged("logger");
            }
        }

        private void ClearExecute()
        {
            logger.LogContent = String.Empty;
        }

    }
}
