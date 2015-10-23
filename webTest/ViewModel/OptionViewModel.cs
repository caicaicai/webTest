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
    public class OptionViewModel : ViewModelBase
    {
        private Option _option;
        private ProxySpider proxySpider;

        public ICommand AddProxy { get; private set; }
        public ICommand RemoveProxy { get; private set; }
        public ICommand ImportProxys { get; private set; }
        public ICommand ExportProxys { get; private set; }
        public ICommand ClearProxys { get; private set; }

        public ICommand RunSpider { get; private set; }
        public ICommand PushIpToOption { get; private set; }
        public ICommand CheckProxy { get; private set; }

        public List<string> badCheckedProxys;



        private string selectedUserAgent;

        public OptionViewModel(Option option)
        {
            _option = option;

            AddProxy = new RelayCommand(() => AddProxyExec(), () => { return true; });
            RemoveProxy = new RelayCommand(() => RemoveProxyExec(), () => { return true; });
            ImportProxys = new RelayCommand(() => ImportProxysExec(), () => { return true; });
            ExportProxys = new RelayCommand(() => ExportProxysExec(), () => { return true; });
            ClearProxys = new RelayCommand(() => ClearProxysExec(), () => { return true; });
            RunSpider = new RelayCommand(() => ProxySpider.run(), () => { return true; });
            PushIpToOption = new RelayCommand(() => PushIpToOptionExec(), () => { return true; });
            CheckProxy = new RelayCommand(() => CheckProxyExec(), () => { return true; });
            badCheckedProxys = new List<string>();

            proxySpider = new ProxySpider();
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
                            //option.Proxys.Clear();
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

        private void CheckProxyExec()
        {
            badCheckedProxys.Clear();
            Parallel.ForEach(option.Proxys, ProxyServer => checkProxy(ProxyServer));
            Console.WriteLine("start to clear bad proxy, count: {0}", badCheckedProxys.Count);

            for (int i = 0; i < option.Proxys.Count; i++)
            {
                if (badCheckedProxys.Contains(option.Proxys[i].Server))
                {
                    option.Proxys.RemoveAt(i);
                }
            }

            Console.WriteLine("clear end...");
        }

        private void checkProxy(ProxyServer proxyServer)
        {
            Console.WriteLine("start to check");
            var request = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");
            string server = proxyServer.Server;
            Match match = new Regex(@"http://").Match(proxyServer.Server);
            if (!match.Success)
            {
                server = "http://" + proxyServer.Server;
            }
            WebProxy myProxy = new WebProxy(proxyServer.Server);
            request.Proxy = myProxy;
            request.Timeout = 5000;
            Console.WriteLine("before try");
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("this should be removed;{0}", proxyServer.Server);
                badCheckedProxys.Add(proxyServer.Server);
            }
            
        }

        private void ClearProxysExec()
        {
            option.Proxys.Clear();

        }

        private void PushIpToOptionExec()
        {
            foreach(string ip in ProxySpider.Ips)
            {
                option.Proxys.Add(new ProxyServer(ip, true));
            }
            string count = proxySpider.Ips.Count.ToString();
            MessageBox.Show(String.Format("success push ips {0} to option.",count));
            proxySpider.Ips.Clear();
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
