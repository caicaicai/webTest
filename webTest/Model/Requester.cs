using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

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
        private Option option;
        private ParameterInterpreter pi;

        public Requester(TabItem _tabItem, Option _option, ParameterInterpreter _pi)
        {
            tabItem = _tabItem;
            option = _option;
            pi = _pi;
        }

        public void DoRequest()
        {
            var targetUri = UriBuilder(pi.generater(tabItem.RequestUrl), pi.generater(tabItem.QueryStr));

            var request = (HttpWebRequest)WebRequest.Create(targetUri);


            //request.Timeout = 5000;
            if(option.Timeout > 0)
            {
                request.Timeout = option.Timeout;
            }

            if(option.Host.Length > 0)
            {
                request.Host = option.Host;
            }

            if (option.UseUserAgent)
            {
                request.UserAgent = pi.generater(option.UserAgent);
            }

            if(option.UseCookie)
            {
                CookieContainer myContainer = new CookieContainer();

                Dictionary<string, string> cookies = new Dictionary<string, string>();

                //the option.coolie should be key:value;key:value;
                foreach(string cookie in option.Cookie.Split(';'))
                {
                    string[] s = cookie.Split('=');
                    cookies.Add(s[0].Trim(), pi.generater(s[1].Trim()));
                }

                foreach(KeyValuePair<string, string> cookie in cookies){
                    myContainer.Add(new Cookie(cookie.Key, cookie.Value) { Domain = targetUri.Host });
                }

                request.CookieContainer = myContainer;
            }

            if(option.UseProxy && option.Proxys.Count > 0){
                WebProxy myProxy = new WebProxy();
                string proxyAddress;

                proxyAddress = option.ProxyServer.Server;
                Regex regex = new Regex(@"http://");
                Match match = regex.Match(proxyAddress);
                if (!match.Success)
                {
                    proxyAddress = "http://" + proxyAddress;
                }
                if (proxyAddress.Length > 0)
                {
                    /*
                    Console.WriteLine("\nPlease enter the Credentials (may not be needed)");
                    Console.WriteLine("Username:");
                    string username;
                    username =Console.ReadLine();
                    Console.WriteLine("\nPassword:");
                    string password;
                    password =Console.ReadLine();
                        */
                    // Create a new Uri object.

                    Uri newUri = new Uri(proxyAddress);
                    myProxy.Address = newUri;

                    request.Proxy = myProxy;
                }
            }
            
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

            //var response = (HttpWebResponse)request.GetResponse();

            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            tabItem.Response = (HttpWebResponse)request.GetResponse();
        }

        public Uri UriBuilder(string urlBase, string queryStr)
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
            if (queryStr.Length > 0){
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
                if (option.UriRewrite.Length > 0)
                {
                    return new Uri(String.Format("{0}{1}{2}{3}{4}", uri.Scheme, Uri.SchemeDelimiter, option.UriRewrite, uri.AbsolutePath, uri.Query));
                }
                return new Uri(String.Format("{0}{1}{2}{3}{4}", uri.Scheme, Uri.SchemeDelimiter, uri.Authority, uri.AbsolutePath, uri.Query));
            }
            if (option.UriRewrite.Length > 0)
            {
                return new Uri(String.Format("{0}{1}{2}{3}{4}", uri.Scheme, Uri.SchemeDelimiter, option.UriRewrite, uri.AbsolutePath, clearQueryStr));
            }
            return new Uri(String.Format("{0}{1}{2}{3}{4}", uri.Scheme, Uri.SchemeDelimiter, uri.Authority, uri.AbsolutePath, clearQueryStr));
        }

    }

}
