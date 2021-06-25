using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettingOptionsDemo.Services
{
    public interface ITestService
    {
        LoggingOption GetLoggingOption();
    }
}
