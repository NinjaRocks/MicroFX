using System;
using System.Configuration;

namespace MicroFx
{
    public class ServiceSettings : IServiceSettings
    {
        public ServiceSettings(SettingsBase baseSettings)
        {
            Port = Convert.ToInt32(baseSettings["Port"]);
            Host = Convert.ToString(baseSettings["Host"]);
            ServiceName = Convert.ToString(baseSettings["ServiceName"]);
            Description = Convert.ToString(baseSettings["Description"]);
        }

        
        public int? Port { get; set; }
        public string Host { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
    }
}