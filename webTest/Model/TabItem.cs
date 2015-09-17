using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;

namespace webTest.Model
{
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

                _note = value;
                RaisePropertyChanged("Title");
            }
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
                    return "请求中..";
                }
                else
                {
                    return "请求";
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
