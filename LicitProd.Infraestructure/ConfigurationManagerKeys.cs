using Newtonsoft.Json;
using System.IO;

namespace LicitProd.Infrastructure
{
    public static class ConfigurationManagerKeys
    {
        private static Configuration _configuration;
        public static string ConnectionString =>
            "Data Source=DESKTOP-L51S99M;Initial Catalog=LicitProd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string BackupsFolder =>
            "c:/backups";

        public static Configuration Configuration()
        {
            if (_configuration == null)
                using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Config.json")))
                    _configuration = JsonConvert.DeserializeObject<Configuration>(sr.ReadToEnd());
            _configuration.BackupsFolder = BackupsFolder;
            return _configuration;
        }
    }
}
