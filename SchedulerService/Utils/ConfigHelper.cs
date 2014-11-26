using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Utils
{
    public class ConfigHelper
    {
        public static string GetAppConfig(string name)
        {
            var temp = ConfigurationManager.AppSettings.Get(name);
            return temp;
        }
    }
}
