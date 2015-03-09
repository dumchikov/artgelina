using System;
using System.IO;
using System.Xml.Serialization;
using artgelina.web.Models;

namespace artgelina.web
{
    public static class ConfigurationManager
    {
        private static readonly string ConfigFile;

        private static readonly XmlSerializer Serializer;

        static ConfigurationManager()
        {
            ConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "config.xml");
            Serializer = new XmlSerializer(typeof(ArtgelinaModel));
        }

        public static void UpdateConfig(ArtgelinaModel config)
        {
            var sww = new StringWriter();
            Serializer.Serialize(sww, config);
            File.WriteAllText(ConfigFile, sww.ToString());
        }

        public static ArtgelinaModel GetConfig()
        {
            using (var reader = new StreamReader(ConfigFile))
            {
                return (ArtgelinaModel)Serializer.Deserialize(reader);
            }
        }
    }
}