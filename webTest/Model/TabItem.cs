using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Messaging;

namespace webTest.Model
{
    [Serializable()]
    public struct MainLabs
    {
        public string TITLE { get; set; }
        public string URL { get; set; }
        public string QUERYSTR { get; set; }
        public string POSTDATA { get; set; }
        public string NOTE { get; set; }
        public MainLabs(string title, string url, string query, string postData, string note)
        {
            TITLE = title;
            URL = url;
            QUERYSTR = query;
            POSTDATA = postData;
            NOTE = note;
        }
    }

    [Serializable()]
    public class TabItem : INotifyPropertyChanged
    {

        #region Constructors
        public TabItem()
        {
            RequestUrl = "http://";
            Title = "NEW";
            ReqMethod = RequestMethod.GET;
            PostData = "";
            QueryStr = "";
            Note = "";
            ResponseContent = @"Right Click The Tab Title To Remove The Tab, Default Tab Can Not Be Removed.
{{RandomNumberWithLength:16}}
{{RandomStringWithLength:16}}
{{RandomNumberAndStringWithLength:16}}
{{Range:10000}}0 - 10000
{{FullRange:10000}}00000 - 10000
                ";

            initLabs();
            Times = 1;

        }
        #endregion

        #region Properties
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title == value)
                    return;

                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private string _note;
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                if (_note == value)
                    return;
                Console.WriteLine(value);
                _note = value;
                RaisePropertyChanged("Title");
            }
        }

        private HttpWebResponse _response;
        public HttpWebResponse Response
        {
            get
            {
                return _response;
            }
            set
            {
                if (_response == value)
                    return;

                _response = value;

                ResponseContent = UnHex(new StreamReader(value.GetResponseStream()).ReadToEnd());
                RaisePropertyChanged("Response");
            }
        }

        public static string UnHex(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            string pattern = @"\\u[0-9a-fA-F]{4}";
            var replaced = Regex.Replace(hexString, pattern, (_match) =>
            {
                return char.ConvertFromUtf32(Int32.Parse(_match.Value.Substring(2, 4), System.Globalization.NumberStyles.HexNumber));
            });
            return replaced.ToString();
        }

        private string _responseContent;
        public string ResponseContent
        {
            get
            {
                return _responseContent;
            }
            set
            {
                if (_responseContent == value)
                    return;
                _responseContent = value;
                RaisePropertyChanged("ResponseContent");
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

        private bool _isRequesting;
        public bool IsRequesting
        {
            get
            {
                return _isRequesting;
            }
            set
            {
                if (_isRequesting == value)
                    return;

                _isRequesting = value;
                RaisePropertyChanged("IsRequesting");
                RaisePropertyChanged("RequestButton");
            }
        }

        public string RequestButton
        {
            get
            {
                if (IsRequesting == true)
                {
                    return "Processing..";
                }
                else
                {
                    return "Request";
                }
            }
        }

        private RequestMethod _reqMethod;
        public RequestMethod ReqMethod
        {
            get
            {
                return _reqMethod;
            }
            set
            {
                if (_reqMethod == value)
                    return;

                _reqMethod = value;
                RaisePropertyChanged("ReqMethod");
                initLabs();
            }
        }

        public Array MethodListData
        {
            get { return Enum.GetValues(typeof(RequestMethod)); }
        }

        private string _userAgent;
        public string UserAgent
        {
            get
            {
                return _userAgent;
            }
            set
            {
                if (_userAgent == value)
                    return;
                _userAgent = value;
                RaisePropertyChanged("UserAgent");
            }
        }

        private string _postData;
        public string PostData
        {
            get
            {
                return _postData;
            }
            set
            {
                if (_postData == value)
                    return;
                _postData = value;
                RaisePropertyChanged("PostData");
            }
        }

        private string _queryStr;
        public string QueryStr
        {
            get
            {
                return _queryStr;
            }
            set
            {
                if (_queryStr == value)
                    return;
                _queryStr = value;
                RaisePropertyChanged("QueryStr");
            }
        }

        private MainLabs mainLab;

        public MainLabs MainLab
        {
            get { return mainLab; }
            set {
                mainLab = value;
                RaisePropertyChanged("MainLab");
            }
        }

        public void initLabs()
        {
            if(ReqMethod.Equals(RequestMethod.SOAP))
            {
                MainLab = new MainLabs("Title", "URL", "SOAPAction", "SOAP-ENV:Body", "Note");
            }
            else
            {
                MainLab = new MainLabs("Title", "URL", "QueryStr", "PostData", "Note");
            }

            Console.WriteLine(MainLab.TITLE);
        }
        private int times;

        public int Times
        {
            get { return times; }
            set {
                times = value;
                RaisePropertyChanged("Times");
            }
        }




        #endregion

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
