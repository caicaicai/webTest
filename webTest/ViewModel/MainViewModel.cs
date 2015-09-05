using System;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Command;
using webTest.Model;


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
        private BackgroundWorker backgroundWorker;
        private string _htmlResponse;
        private string _requestUrl;
        private string _requestBtn;

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

            ShowPopUp = new RelayCommand(() => ShowPopUpExecute(), () => true);

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            RequestUrl = "http://";
            HtmlResponse = "";
            RequestBtn = "Request";
            
        }

        private void ShowPopUpExecute()
        {
            Requester rq = new Requester(RequestUrl);
            RequestBtn = "Requesting...";
            try
            {
                backgroundWorker.RunWorkerAsync(rq);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        
        public string HtmlResponse
        {
            get
            {
                return _htmlResponse;
            }
            set
            {
                if (_htmlResponse == value)
                    return;
                _htmlResponse = value;
                RaisePropertyChanged("HtmlResponse");
            }
        }

        public string RequestUrl
        {
            get
            {
                return _requestUrl;
            }
            set
            {
                if (_requestUrl == value)
                    return;
                _requestUrl = value;
                RaisePropertyChanged("RequestUrl");
            }
        }

        public string RequestBtn
        {
            get
            {
                return _requestBtn;
            }
            set
            {
                if (_requestBtn == value)
                    return;
                _requestBtn = value;
                RaisePropertyChanged("RequestBtn");
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Requester rq = (Requester)e.Argument;
                // Return the value through the Result property.
                e.Result = rq.DoRequest();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Access the result through the Result property. 
            HtmlResponse = (string)e.Result;
            RequestBtn = "Request Done!";
        }
    }
}