using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;

namespace SettingProvidersDemo
{



    class Program
    {
        static void Main0(string[] args)
        {
            ///Commonly Used Types:
            ///Microsoft.Extensions.Configuration.IConfiguration
            ///Microsoft.Extensions.Configuration.IConfigurationBuilder
            ///Microsoft.Extensions.Configuration.IConfigurationProvider
            ///Microsoft.Extensions.Configuration.IConfigurationRoot
            ///Microsoft.Extensions.Configuration.IConfigurationSection

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "key1","value1"},
                { "key2","value2"},
                { "key3","value3"},
                { "key4","value4"},
                { "section1:key5","value5"},
                { "section1:key6","value6"}
            });

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            Console.WriteLine($"key1:{configurationRoot["key1"]}");
            Console.WriteLine($"key5:{configurationRoot["key5"]}");

            IConfigurationSection section1 = configurationRoot.GetSection("section1");

            Console.WriteLine($"section1:key5:{section1["key5"]}");
            Console.WriteLine($"section1:key6:{section1["key6"]}");
        }

        /// <summary>
        /// 命令行配置提供程序 Microsoft.Extensions.Configuration.CommandLine
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            var mapping = new Dictionary<string, string>()
            {
                { "-key5", "key2" }
            };

            configurationBuilder.AddCommandLine(args, mapping);

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            Console.WriteLine($"key1:{configurationRoot["key1"]}");
            Console.WriteLine($"key2:{configurationRoot["key2"]}");
            Console.WriteLine($"key3:{configurationRoot["key3"]}");
            Console.WriteLine($"key4:{configurationRoot["key4"]}");
            Console.WriteLine($"key5:{configurationRoot["key5"]}");
        }

        /// <summary>
        /// 环境变量配置提供程序 Microsoft.Extensions.Configuration.EnvironmentVariables
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddEnvironmentVariables();

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            Console.WriteLine($"key1:{configurationRoot["key1"]}");
            Console.WriteLine($"key2:{configurationRoot["key2"]}");
            Console.WriteLine($"OneDrive:{configurationRoot["OneDrive"]}");
            Console.WriteLine($"key3:{configurationRoot["key3"]}");
            Console.WriteLine($"key4:{configurationRoot["key4"]}");


            #region 分层键

            IConfigurationSection envSection = configurationRoot.GetSection("env");

            Console.WriteLine($"key3:{envSection["key3"]}");

            #endregion

            #region 指定前缀

            IConfigurationBuilder builder = new ConfigurationBuilder();

            builder.AddEnvironmentVariables("env_");

            IConfigurationRoot root = builder.Build();

            // 双下滑线标记的分层键不能用于过滤指定前缀
            Console.WriteLine($"key3:{root["_key3"]}");

            IConfigurationSection envSection2 = root.GetSection("env");

            Console.WriteLine($"key3:{envSection2["key3"]}");

            Console.WriteLine($"key4:{root["key4"]}");

            #endregion


        }


        /// <summary>
        /// 文件配置提供程序 
        /// </summary>
        /// <param name="args"></param>
        static void Main3(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true);

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            var config = new Config();

            // Microsoft.Extensions.Configuration.Binder
            // 绑定强类型对象
            configurationRoot.Bind(config);

            // 配置热更新
            IChangeToken changeToken = configurationRoot.GetReloadToken();

            // 只适用一次变化，需重新获取changeToken后重新注册
            //changeToken.RegisterChangeCallback(root =>
            //{
            //    var rootObj = root as IConfigurationRoot;
            //    Console.WriteLine("文件发生了变化");
            //    Console.WriteLine($"新值AllowedHosts:{rootObj["AllowedHosts"]}");
            //}, configurationRoot);

            // 可以一直检测文件内容发生变化
            ChangeToken.OnChange(() =>
                {
                    return configurationRoot.GetReloadToken();
                }, root =>
                {
                    Console.WriteLine("文件发生了变化");
                    Console.WriteLine($"新值AllowedHosts:{root["AllowedHosts"]}");
                }, configurationRoot);

            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.WriteLine($"AllowedHosts:{configurationRoot["AllowedHosts"]}");
            }


        }


        /// <summary>
        /// 自己扩展的配置提供程序
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddMyConfigurationSource();

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            Console.WriteLine(configurationRoot["lastTime"]);

            Console.ReadLine();
        }


        public class Config
        {
            public Logging Logging { get; set; }
            public string AllowedHosts { get; set; }
        }

        public class Logging
        {
            public Loglevel LogLevel { get; set; }
        }

        public class Loglevel
        {
            public string Default { get; set; }
            public string Microsoft { get; set; }
            public string MicrosoftHostingLifetime { get; set; }
        }


    }
}
