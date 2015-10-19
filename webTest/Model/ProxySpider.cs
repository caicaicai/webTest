using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace webTest.Model
{
    public class ProxySpider : INotifyPropertyChanged
    {
        private string targetServer;
        private string spiderLog;
        private int depth;
        private int currentDepth;

        private List<string> visited;
        private List<string> urls;
        private List<string> glob_visited;

        private List<string> ips;


        public ProxySpider()
        {
            TargetServer = "http://www.youdaili.net/";
            SpiderLog = "";
            Depth = 1;
            currentDepth = 0;

            visited = new List<string>();
            urls = new List<string>();
            glob_visited = new List<string>();
            ips = new List<string>();

            

        }

        public void run()
        {
            SpiderLog = "";
            currentDepth = 0;
            visited.Clear();
            urls.Clear();
            glob_visited.Clear();
            Ips.Clear();
            glob_visited.Add(targetServer);

            SpiderLog += String.Format("start fetch..") + "\n";

            while (currentDepth < Depth){
                foreach (string url in glob_visited)
                {
                    if (!visited.Contains(url))
                    {
                        visited.Add(url);
                        urls.Add(url);
                    }
                }
                glob_visited.Clear();

                Parallel.ForEach(urls, url => fetch(url));
                urls.Clear();
                this.currentDepth++;
               
            }

            SpiderLog += String.Format("fetch end..") + "\n";
            SpiderLog += String.Format("count {0} ips get.", Ips.Count) + "\n";
            Console.WriteLine("fetch end..");
        }

        public void fetch(string url)
        {
            Console.WriteLine("{0}, Thread Id= {1}", url, Thread.CurrentThread.ManagedThreadId);
            
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.UserAgent = "Mozilla/5.0 (X11; Linux i686) AppleWebKit/535.7 (KHTML, like Gecko) Ubuntu/11.04 Chromium/16.0.912.77 Chrome/16.0.912.77 Safari/535.7";
                request.Referer = "http://www.google.com";

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream,Encoding.UTF8);
                string responseFromServer = reader.ReadToEnd();
                response.Close();

                Match proxyMatch = new Regex(@"([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}:[0-9]{1,5})", RegexOptions.IgnoreCase).Match(responseFromServer);

                while (proxyMatch.Success)
                {
                    Group g = proxyMatch.Groups[0];
                    Thread.MemoryBarrier();
                    Ips.Add(g.ToString());
                    Thread.MemoryBarrier();

                    proxyMatch = proxyMatch.NextMatch();
                }

                Match httpMatch = new Regex(@"<(?<Tag_Name>(a))\b[^>]*?\b(?<URL_Type>(?(1)href))\s*=\s*(?:""(?<URL>(?:\\""|[^""])*)""|'(?<URL>(?:\\'|[^'])*)')", RegexOptions.IgnoreCase).Match(responseFromServer);
                while (httpMatch.Success)
                {
                    Thread.MemoryBarrier();
                    glob_visited.Add(httpMatch.Groups[4].ToString());
                    Thread.MemoryBarrier();

                    httpMatch = httpMatch.NextMatch();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public string TargetServer
        {
            get
            {
                return targetServer;
            }
            set
            {
                if (targetServer == value)
                    return;

                targetServer = value;
                RaisePropertyChanged("TargetServer");
            }
        }

        public string SpiderLog
        {
            get
            {
                return spiderLog;
            }
            set
            {
                if (spiderLog == value)
                    return;

                spiderLog = value;
                RaisePropertyChanged("SpiderLog");
            }
        }

        public int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                if (depth == value)
                    return;

                depth = value;
                RaisePropertyChanged("Depth");
            }
        }

        public List<string> Ips
        {
            get
            {
                return ips;
            }
            set
            {
                if (ips == value)
                    return;

                ips = value;
                RaisePropertyChanged("Ips");
            }
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
