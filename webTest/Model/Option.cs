using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace webTest.Model
{
    [Serializable()]
    public class ProxyServer{
        public string Server{ get; set;}
        public bool Available { get; set; }
        public ProxyServer(string server, bool available)
        {
            this.Server = server;
            this.Available = available;
        }
    }

    [Serializable()]
    public class Option : INotifyPropertyChanged
    {
        #region proxy property
        public int currentProxy;
        #endregion

        private string cookie;
        private bool useCookie;
        private string userAgent;
        private bool useUserAgent;
        private Dictionary<string,string> userAgentTemplate;

        private ObservableCollection<ProxyServer> proxys;
        private int selectedPorxyIndex;
        private bool useProxy;
        private bool autoNextProxy;

        private bool useUriRewrite;
        private string uriRewrite;

        private int requestThread;

        public int RequestThread
        {
            get { return requestThread; }
            set
            {
                requestThread = value;
                RaisePropertyChanged("RequestThread");
            }
        }

        private int requestTimes;

        public int RequestTimes
        {
            get { return requestTimes; }
            set
            {
                requestTimes = value;
                RaisePropertyChanged("RequestTimes");
            }
        }


        #region 构造函数
        public Option()
        {

            // ++ before its been return
            currentProxy = -1;

            Cookie = "key1=value1;key2=value2";
            UseCookie = false;

            UserAgentTemplate = new Dictionary<string, string>();
            UserAgentTemplate.Add("Chrome", "Mozilla/5.0 (Windows NT 5.2) AppleWebKit/534.30 (KHTML, like Gecko) Chrome/12.0.742.122 Safari/534.30");
            UserAgentTemplate.Add("Firefox", "Mozilla/5.0 (Windows NT 5.1; rv:5.0) Gecko/20100101 Firefox/5.0");
            UserAgentTemplate.Add("IE8", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET4.0E; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C)");
            UserAgentTemplate.Add("IE7", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET4.0E; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C)");
            UserAgentTemplate.Add("IE6", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)");
            UserAgentTemplate.Add("IE9", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)");
            UserAgentTemplate.Add("Opera", "Opera/9.80 (Windows NT 5.1; U; zh-cn) Presto/2.9.168 Version/11.50");
            UserAgentTemplate.Add("360 safe Browser in IE6", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)");
            UserAgentTemplate.Add("360 safe Browser in IE8", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0E; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C)");
            UserAgentTemplate.Add("Safari", "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN) AppleWebKit/533.21.1 (KHTML, like Gecko) Version/5.0.5 Safari/533.21.1");
            UserAgentTemplate.Add("Maxthon", "Mozilla/5.0 (Windows; U; Windows NT 5.1; ) AppleWebKit/534.12 (KHTML, like Gecko) Maxthon/3.0 Safari/534.12");
            UserAgentTemplate.Add("TheWorld", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; TheWorld)");
            UserAgent = UserAgentTemplate["Chrome"];
            UseUserAgent = false;

            Proxys = new ObservableCollection<ProxyServer>();
            Proxys.Add(new ProxyServer("127.0.0.1:1080", true ));
            SelectedProxyIndex = 0;
            UseProxy = false;
            AutoNextProxy = false;

            UriRewrite = "";

            RequestThread = 1;
            RequestTimes = 1;

            Host = "";
            Timeout = 0;
            
        }
        #endregion
        

        public string Cookie
        {
            get
            {
                return cookie;
            }
            set
            {
                if (cookie == value)
                    return;

                cookie = value;
                RaisePropertyChanged("Cookie");
            }
        }
        public bool UseCookie
        {
            get
            {
                return useCookie;
            }
            set
            {
                if (useCookie == value)
                    return;

                useCookie = value;
                RaisePropertyChanged("UseCookie");
            }
        }
        public bool UseUserAgent
        {
            get
            {
                return useUserAgent;
            }
            set
            {
                if (useUserAgent == value)
                    return;

                useUserAgent = value;
                RaisePropertyChanged("UseUserAgent");
            }
        }
        public string UserAgent
        {
            get
            {
                return userAgent;
            }
            set
            {
                if (userAgent == value)
                    return;

                userAgent = value;
                RaisePropertyChanged("UserAgent");
            }
        }
        public Dictionary<string, string> UserAgentTemplate
        {
            get
            {
                return userAgentTemplate;
            }
            set
            {
                if (userAgentTemplate == value)
                    return;

                userAgentTemplate = value;
                RaisePropertyChanged("UserAgentTemplate");
            }
        }

        public ProxyServer ProxyServer
        {
            get
            {
                int current = SelectedProxyIndex;
                if (AutoNextProxy)
                {
                    if (SelectedProxyIndex == Proxys.Count - 1)
                    {
                        SelectedProxyIndex = 0;
                    }
                    else
                    {
                        SelectedProxyIndex++;
                    }
                }
                return proxys[current];
            }
        }

        public ObservableCollection<ProxyServer> Proxys
        {
            get
            {
                return proxys;
            }
            set
            {
                if (proxys == value)
                    return;
                proxys = value;
                RaisePropertyChanged("Proxys");
            }
        }

        public int SelectedProxyIndex
        {
            get
            {
                return selectedPorxyIndex;
            }
            set
            {
                if (selectedPorxyIndex == value)
                    return;
                selectedPorxyIndex = value;
                RaisePropertyChanged("SelectedProxyIndex");
            }
        }

        public bool UseProxy
        {
            get
            {
                return useProxy;
            }
            set
            {
                if (useProxy == value)
                    return;

                useProxy = value;
                RaisePropertyChanged("UseProxy");
            }
        }

        public bool AutoNextProxy
        {
            get
            {
                return autoNextProxy;
            }
            set
            {
                if (autoNextProxy == value)
                    return;

                autoNextProxy = value;
                RaisePropertyChanged("AutoNetxProxy");
            }
        }

        public string UriRewrite
        {
            get
            {
                return uriRewrite;
            }
            set
            {
                if (uriRewrite == value)
                    return;

                uriRewrite = value;
                RaisePropertyChanged("UriRewrite");
            }
        }

        public bool UseUriRewrite
        {
            get
            {
                return useUriRewrite;
            }
            set
            {
                if (useUriRewrite == value)
                    return;
                useUriRewrite = value;
                RaisePropertyChanged("UseUriRewrite");
            }
        }


        private string host;

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        private int timeout;

        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }




        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
