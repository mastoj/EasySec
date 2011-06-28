using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EasySec
{
    public class EasySecConfigSection : ConfigurationSection
    {
        private static EasySecConfigSection settings = ConfigurationManager.GetSection("easySecSettings") as EasySecConfigSection;
        public static EasySecConfigSection Settings
        {
            get { return settings; }
        }

        [ConfigurationProperty("userName", DefaultValue = "", IsRequired = true)]
        public string UserName
        {
            get
            {
                return this["userName"] as string;
            }
            set
            {
                this["userName"] = value;
            }
        }
        [ConfigurationProperty("password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get
            {
                return this["password"] as string;
            }
            set
            {
                this["password"] = value;
            }
        }

        
    }
}
