using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SettingOptionsDemo.CustomValidateOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettingOptionsDemo.Services
{
    public static class TestServiceExternsions
    {
        public static IServiceCollection AddTestService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<LoggingOption>(configuration.GetSection("Logging"));

            services.AddOptions<LoggingOption>().Configure(options =>
            {
                configuration.GetSection("Logging").Bind(options);
            })
            //.Validate(options => options.LogLevel.Default == "Error","值不为Error"); // 1.直接验证的方式
            //.ValidateDataAnnotations(); // 2.属性标签验证的方式 嵌套类型的验证需要自定义，直接标记将不起效果
            .Services.AddSingleton<IValidateOptions<LoggingOption>, LoggingValidateOption>(); // 3.自定义验证方式，实现IValidateOptions

            // 读取配置后在额外修改
            services.PostConfigure<LoggingOption>(options =>
            {
                options.LogLevel.Default += "test";
            });

            services.AddScoped<ITestService, TestService>();

            return services;

        }
    }
}
