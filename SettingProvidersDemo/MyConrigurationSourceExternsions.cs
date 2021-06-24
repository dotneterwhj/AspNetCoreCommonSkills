using SettingProvidersDemo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static class MyConrigurationSourceExternsions
    {
        public static IConfigurationBuilder AddMyConfigurationSource(this IConfigurationBuilder configurationBuilder)
        {
            var builder = configurationBuilder.Add(new MyConfigurationSource());
            return builder;
        }
    }
}
