using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettingOptionsDemo.Services
{

    /// <summary>
    /// IOptions<>是单例的，它不跟踪配置更新，读不到更新后的值。
    /// IOptionsMonitor<> 是单例的，它跟踪配置更新，总是读到最新的值。
    /// IOptionsSnapshot<> 是范围的，它在范围的生命周期中，不会更新，但它会读到范围生命周期创建前的变更。
    /// </summary>
    public class TestService : ITestService
    {
        private readonly IOptionsMonitor<LoggingOption> _loggingOption;

        public TestService(IOptionsMonitor<LoggingOption> loggingOption)
        {
            this._loggingOption = loggingOption;

            _loggingOption.OnChange(option => 
            {
                Console.WriteLine($"配置发生了变化，新值为：{option.LogLevel.Default}");
            });
        }

        public LoggingOption GetLoggingOption()
        {
            return _loggingOption.CurrentValue;
        }
    }
}
