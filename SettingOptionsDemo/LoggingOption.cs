using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SettingOptionsDemo
{
    public class LoggingOption
    {
        [Range(1,20)]
        public int MaxCount { get; set; }

        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        [Range(1,5)]
        public int MinCount { get; set; }

        [MaxLength(2)]
        public string Default { get; set; } = "111";
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

}
