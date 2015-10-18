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
        private ProxySpider proxySpider;

        public ICommand AddProxy { get; private set; }
        public ICommand RemoveProxy { get; private set; }
        public ICommand ImportProxys { get; private set; }
        public ICommand ExportProxys { get; private set; }

        public ICommand RunSpider { get; private set; }


        private string selectedUserAgent;

        public OptionViewModel(Option option)
        {
            this._option = option;

            this.AddProxy = new RelayCommand(() => AddProxyExec(), () => { return true; });
            this.RemoveProxy = new RelayCommand(() => RemoveProxyExec(), () => { return true; });
            this.ImportProxys = new RelayCommand(() => ImportProxysExec(), () => { return true; });
            this.ExportProxys = new RelayCommand(() => ExportProxysExec(), () => { return true; });
            this.RunSpider = new RelayCommand(() => this.ProxySpider.run(), () => { return true; });

            this.proxySpider = new ProxySpider();
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
                option.SelectedProxyIndex = 0;
            }
        }

        private void ImportProxysExec()
        {
            Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader reader = new StreamReader(myStream);
                            string line;
                            option.Proxys.Clear();
                            while ((line = reader.ReadLine()) != null)
                            {
                                option.Proxys.Add(new ProxyServer(line, true));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ExportProxysExec()
        {
            Stream myStream;
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "proxys.txt";

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    StreamWriter writer = new StreamWriter(myStream);

                    foreach (ProxyServer proxy in option.Proxys)
                        writer.WriteLine(proxy.Server);

                    writer.Close();
                    myStream.Close();
                }
            }

        }

        public ProxySpider ProxySpider
        {
            get
            {
                return this.proxySpider;
            }
            set
            {
                if (this.proxySpider == value)
                    return;

                this.proxySpider = value;
                RaisePropertyChanged("ProxySpider");
            }
        }

    }
}
