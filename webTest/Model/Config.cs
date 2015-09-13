using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace webTest.Model
{
    class Config
    {
        public ObservableCollection<TabItem> TabItems;
        
        public static Config Load(string config_file_path)
        {
            try
            {
                string configContent = File.ReadAllText(config_file_path);
                Config config = SimpleJson.SimpleJson.DeserializeObject<Config>(configContent, new JsonSerializerStrategy());
                return config;
            }
            catch (Exception e)
            {
                if (!(e is FileNotFoundException))
                {
                    Console.WriteLine(e);
                }
                MessageBox.Show("配置文件读取失败!{0}",e.Message);
                return new Config
                {
                    TabItems = new ObservableCollection<TabItem>(){ }
                };
            }
        }

        public static void Save(string config_file_path, Config config)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(config_file_path, FileMode.Create)))
                {
                    string jsonString = SimpleJson.SimpleJson.SerializeObject(config);
                    sw.Write(jsonString);
                    sw.Flush();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e);
            }
        }

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
