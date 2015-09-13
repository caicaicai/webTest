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
        private string _title;
        private string _responseContent;
        private string _requestUrl;
        private bool _isRequesting;

        public TabItem()
        {
            RequestUrl = "http://";
            Title = "NEW";
        }

        public string Title {
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

    }
}
