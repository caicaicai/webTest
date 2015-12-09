using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace webTest.Model
{
    [Serializable()]
    class Config
    {
        public ObservableCollection<TabItemsGroup> TabItemsGroup;
        public Option option;

        public Config(ObservableCollection<TabItemsGroup> TabItemsGroup, Option option)
        {
            this.TabItemsGroup = TabItemsGroup;
            this.option = option;
        }

        public Config(SerializationInfo info, StreamingContext ctxt)
        {
            this.TabItemsGroup = (ObservableCollection<TabItemsGroup>)info.GetValue("TabItemsGroup", typeof(ObservableCollection<TabItemsGroup>));
            this.option = (Option)info.GetValue("option", typeof(Option));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("TabItemsGroup", this.TabItemsGroup);
            info.AddValue("option", this.option);
        }
        
        public static Config Load(string config_file_path)
        {
            webTest.Properties.Settings.Default.configPath = config_file_path;
            try
            {
                /*
                string configContent = File.ReadAllText(config_file_path);
                Config config = SimpleJson.SimpleJson.DeserializeObject<Config>(configContent, new SimpleJson.PocoJsonSerializerStrategy());
                return config;
                 * */

                //Open the file written above and read values from it.
                Stream stream = File.Open(config_file_path, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();

                Console.WriteLine("Reading Config Information");
                Config config = (Config)bformatter.Deserialize(stream);
                stream.Close();
                return config;
            }
            catch (Exception e)
            {
                if (!(e is FileNotFoundException))
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine(e.Message);
                MessageBox.Show("配置文件读取失败!{0}",e.Message);
                return new Config(new ObservableCollection<TabItemsGroup>(), new Option());
            }
        }

        public static void Save(string config_file_path, Config config)
        {
            webTest.Properties.Settings.Default.configPath = config_file_path;
            try
            {
                Stream stream = File.Open(config_file_path, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();

                Console.WriteLine("Writing Config Information");
                bformatter.Serialize(stream, config);
                stream.Close();
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e);
            }
        }

    }
}
