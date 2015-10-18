using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.TargetServer = "http://www.youdaili.net/Daili/http/3736.html";
            this.SpiderLog = "";
            this.Depth = 2;
            this.currentDepth = 0;

            this.visited = new List<string>();
            this.urls = new List<string>();
            this.glob_visited = new List<string>();
            this.ips = new List<string>();

            

        }

        public void run()
        {
            this.glob_visited.Add(this.targetServer);
            while(this.currentDepth < this.depth){
                foreach (string url in this.glob_visited)
                {
                    if (!this.visited.Contains(url))
                    {
                        this.visited.Add(url);
                        this.urls.Add(url);
                    }
                }
                this.glob_visited.Clear();

                foreach (string url in this.urls)
                {
                    Console.WriteLine("fetching {0}", url);
                    this.fetch(url);
                }
                this.urls.Clear();

                Console.WriteLine("current fetch depth {0}", this.currentDepth);
                this.currentDepth++;
               
            }
        }

        public void fetch(string url)
        {
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

                Console.WriteLine("perpareing regx");
                Console.WriteLine(responseFromServer);
                Match proxyMatch = new Regex(@"(\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}:\d{1,5})", RegexOptions.IgnoreCase).Match(responseFromServer);

                Console.WriteLine("match:{0}", proxyMatch.Success);
                int matchCount = 0;
                while (proxyMatch.Success)
                {
                    Console.WriteLine("Match" + (++matchCount));
                    for (int i = 1; i <= 2; i++)
                    {
                        Group g = proxyMatch.Groups[i];
                        Console.WriteLine("Group" + i + "='" + g + "'");
                        CaptureCollection cc = g.Captures;
                        for (int j = 0; j < cc.Count; j++)
                        {
                            Capture c = cc[j];
                            System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                        }
                    }
                    proxyMatch = proxyMatch.NextMatch();
                }

                Match httpMatch = new Regex("(?<Protocol>w+)://(?<Domain>[w@][w.:@]+)/?[w.?=%&=-@/$,]*", RegexOptions.IgnoreCase).Match(responseFromServer);
                int httpCount = 0;
                while (httpMatch.Success)
                {
                    Console.WriteLine("Match" + (++httpCount));
                    for (int i = 1; i <= 2; i++)
                    {
                        Group g = httpMatch.Groups[i];
                        Console.WriteLine("Group" + i + "='" + g + "'");
                        CaptureCollection cc = g.Captures;
                        for (int j = 0; j < cc.Count; j++)
                        {
                            Capture c = cc[j];
                            System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                        }
                    }
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
