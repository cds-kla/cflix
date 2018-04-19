using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Services
{
    public class CFlixConfiguration
    {
        public CFlixConfiguration()
        {
        }

        public int Stage { get; set; }

        public string ImageAPI { get; set; }

        public bool UseLdap { get; set; }
        
        public bool UseRedis { get; set; }

        public string HackonymousoflixDomain { get; set; }
    }
}
