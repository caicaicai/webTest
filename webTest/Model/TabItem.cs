using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace webTest.Model
{
    public class TabItem : INotifyPropertyChanged
    {

        #region Constructors
        public TabItem()
        {
            RequestUrl = "http://";
            Title = "NEW";
            ReqMethod = RequestMethod.GET;
            MethodListData = Enum.GetValues(typeof(RequestMethod));
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

        private Array _methodListData;
        public Array MethodListData
        {
            get { return _methodListData; }
            set
            {
                _methodListData = value;
                RaisePropertyChanged("MethodListData");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

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

        #region private class JsonSerializerStrategy
        private class JsonSerializerStrategy : SimpleJson.PocoJsonSerializerStrategy
        {
            // convert string to int
            public override object DeserializeObject(object value, Type type)
            {
                if (type == typeof(Int32) && value.GetType() == typeof(string))
                {
                    return Int32.Parse(value.ToString());
                }
                return base.DeserializeObject(value, type);
            }
        }
        #endregion

    }
}
