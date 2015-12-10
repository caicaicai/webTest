using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace webTest.Model
{
    class SOAP: RequestBase
    {
        public TabItem tabItem;

        public  SOAP(TabItem ti)
        {
            tabItem = ti;
        }

        public override void DoRequest()
        {
            log.logWithTime(String.Format("Start--------------------------------------------"));
            log.log(String.Format("URL:  {0}", tabItem.RequestUrl));
            log.log(String.Format("SOAPAction:  {0}", tabItem.QueryStr));
            log.log(String.Format("SOAP-ENV:Body:  {0}", tabItem.PostData));
            log.log(String.Format("Method:  {0}", tabItem.ReqMethod));
            log.log(String.Format("RequestCount:  {0}", tabItem.Times));
            var watch = Stopwatch.StartNew();
            while (tabItem.Times > 0)
            {
                tabItem.ResponseContent = "........";
                tabItem.ResponseContent = CallWebService(tabItem.RequestUrl, tabItem.QueryStr, tabItem.PostData);
                tabItem.Times--;
                
            }
            tabItem.Times = 1;
            log.log(String.Format("Total Calls Time : {0} ms.", watch.ElapsedMilliseconds.ToString()));
            log.logWithTime(String.Format("End---------------------------------------------"));

        }
        public  string CallWebService(string url, string action, string body)
        {
            var _url = url;
            var _action = action;

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(body);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            log.log(String.Format("----Request--{0}--", tabItem.Times));
            var watch = Stopwatch.StartNew();
            

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                watch.Stop();
                log.log(String.Format("Calls Time : {0} ms", watch.ElapsedMilliseconds.ToString()));
                log.log(String.Format("----Response--{0}--", tabItem.Times));
                log.log(String.Format("Headers : {0}", tabItem.Response.Headers));
                log.log(String.Format("CharacterSet : {0}.", tabItem.Response.CharacterSet));
                log.log(String.Format("Method : {0}", tabItem.Response.Method));
                log.log(String.Format("ProtocolVersion : {0}", tabItem.Response.ProtocolVersion));
                log.log(String.Format("ResponseUri : {0}", tabItem.Response.ResponseUri));
                log.log(String.Format("StatusCode : {0}", tabItem.Response.StatusCode));
                log.log(String.Format("StatusDescription : {0}", tabItem.Response.StatusDescription));

                return soapResult;
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string body)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body>"+ body +"</SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

    }
}
