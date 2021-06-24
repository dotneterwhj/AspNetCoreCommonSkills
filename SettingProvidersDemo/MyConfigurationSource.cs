using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SettingProvidersDemo
{

    /***
     * 实现IConfigurationSource
     * 实现IConfigurationProvider
     * service.Addxxx();
     *
     */

    /// <summary>
    /// 自己的配置源
    /// </summary>
    class MyConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyConfigurationProvider();
        }
    }

    class MyConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Load(false);
        }

        private void Load(bool reload)
        {
            base.Data["lastTime"] = DateTime.Now.ToString();

            if (reload)
            {
                base.OnReload();
            }
        }
    }
}
