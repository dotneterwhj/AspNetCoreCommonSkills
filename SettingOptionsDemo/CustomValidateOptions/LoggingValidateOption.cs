using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettingOptionsDemo.CustomValidateOptions
{
    public class LoggingValidateOption : IValidateOptions<LoggingOption>
    {
        public ValidateOptionsResult Validate(string name, LoggingOption options)
        {
            if (options.LogLevel.Default.Length > 3)
            {
                return ValidateOptionsResult.Fail("LogLevel.Default长度不能大于3");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
