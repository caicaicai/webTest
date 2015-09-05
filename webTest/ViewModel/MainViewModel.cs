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
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        private BackgroundWorker backgroundWorker;

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
            RequestUrl = "http://";
            HtmlResponse = "";
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        public ICommand ShowPopUp { get; private set; }

        private void ShowPopUpExecute()
        {
            Requester rq = new Requester(RequestUrl);
            try
            {
                backgroundWorker.RunWorkerAsync(rq);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private string _htmlResponse;
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

        private string _requestUrl;
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
        }
    }
}