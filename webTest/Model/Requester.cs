using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;

namespace webTest.Model
{
    #region Method GET, HEAD, POST, PUT, DELETE, TRACE, or OPTIONS.
    [Serializable]
    public enum RequestMethod
    {
        GET,
        HEAD,
        POST,
        PUT,
        DELETE,
        TRACE,
        OPTIONS
    };
    #endregion

    class Requester
    {
        private TabItem tabItem;

        public Requester(TabItem _tabItem)
        {
            tabItem = _tabItem;
        }

        public void DoRequest()
        {


            var targetUri = UriBuilder(tabItem.RequestUrl, tabItem.QueryStr);

            var request = (HttpWebRequest)WebRequest.Create(targetUri);
            request.Method = Enum.GetName(typeof(RequestMethod), tabItem.ReqMethod);

            if(tabItem.ReqMethod == RequestMethod.POST)
            {
                //string postData = "This is a test that posts this string to a Web server.";
                byte[] byteArray = Encoding.UTF8.GetBytes(tabItem.PostData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();

            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            tabItem.Response = (HttpWebResponse)request.GetResponse();
        }

        public static Uri UriBuilder(string urlBase, string queryStr)
        {
            Uri uri = new Uri(urlBase);
            /*
            Console.WriteLine(uri.Query);
            Console.WriteLine(uri.PathAndQuery);
            Console.WriteLine(uri.AbsolutePath);
            Console.WriteLine(uri.AbsoluteUri);
            Console.WriteLine(uri.GetLeftPart(UriPartial.Path));
            string path = String.Format("{0}{1}{2}{3}", uri.Scheme, Uri.SchemeDelimiter, uri.Authority, uri.AbsolutePath);
            Console.WriteLine(path);

            ?a=1
            /method.php?a=1
            /method.php
            http://dl.xiaocaicai.com/method.php?a=1
            http://dl.xiaocaicai.com/method.php
            http://dl.xiaocaicai.com/method.php
            */
            var clearQueryStr = "";
            if(queryStr.Length > 0){
                if (uri.Query.Length > 0)
                {

                    if (urlBase.StartsWith("&"))
                    {
                        clearQueryStr = uri.Query + queryStr;
                    }
                    else
                    {
                        clearQueryStr = uri.Query + "&" + queryStr;
                    }

                }
                else
                {
                    if (urlBase.StartsWith("?"))
                    {
                        clearQueryStr = queryStr;
                    }
                    else
                    {
                        clearQueryStr = "?" + queryStr;
                    }

                }
            }
            else
            {
                return new Uri(urlBase);
            }
            return new Uri(String.Format("{0}{1}{2}{3}{4}", uri.Scheme, Uri.SchemeDelimiter, uri.Authority, uri.AbsolutePath, clearQueryStr));
        }

    }

}
