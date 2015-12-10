using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;

namespace webTest.Model
{
    public class Logger : INotifyPropertyChanged
    {

        private string logContent;

        public string LogContent
        {
            get { return logContent; }
            set {
                logContent = value;
                RaisePropertyChanged("LogContent");
            }
        }

        public Logger()
        {
            LogContent = String.Empty;
        }

        public void log(string str)
        {
            LogContent +=  str + Environment.NewLine;

            StreamWriter sw = File.AppendText(Directory.GetCurrentDirectory() + "/log.txt");
            sw.WriteLine(str);
            sw.Close();
        }

        public void logInline(string str)
        {
            LogContent += str;

            StreamWriter sw = File.AppendText(Directory.GetCurrentDirectory() + "/log.txt");
            sw.Write(str);
            sw.Close();
        }

        public void logWithTime(string str)
        {
            LogContent += DateTime.Now.ToString("HH:mm:ss  ") + str + Environment.NewLine;

            StreamWriter sw = File.AppendText(Directory.GetCurrentDirectory() + "/log.txt");
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss  ") + str);
            sw.Close();
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
