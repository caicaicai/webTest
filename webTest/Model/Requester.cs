using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace webTest.Model
{
    class Requester
    {
        private TabItem tabItem;

        public Requester(TabItem _tabItem)
        {
            tabItem = _tabItem;
        }

        public void DoRequest()
        {
            var request = (HttpWebRequest)WebRequest.Create(tabItem.RequestUrl);
            var response = (HttpWebResponse)request.GetResponse();
            tabItem.ResponseContent =  new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

    }
}
