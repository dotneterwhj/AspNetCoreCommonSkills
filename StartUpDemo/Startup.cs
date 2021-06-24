using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StartUpDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Console.WriteLine("Startup ctor");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Startup ConfigureServices");

            /// AspNetCore依赖注入框架核心类型
            /// IServiceCollection  负责服务的注册
            /// ServiceDescriptor   服务的信息
            /// IServiceProvider    容器
            /// IServiceScope       容器子容器的生命周期

            #region 普通依赖注入

            services.AddSingleton<ISingletonTestService, SingletonTestService>();
            services.AddScoped<IScopedTestService, ScopedTestService>();
            services.AddTransient<ITrainstantTestService, TrainstantTestService>();

            // 瞬时与范围注册时，对象的生命周期与请求保持一致
            // 不建议在根容器获取注册的实现了IDispose接口的瞬时服务，因为根容器获取的服务只有当应用程序退出时才会回收
            // services.AddTransient<OrderService>();
            // services.AddScoped<IOrderService>(provider => new OrderService());


            // 单例模式注册时,对象的生命周期跟随容器的生命周期
            // services.AddSingleton<OrderService>();
            // 单例模式时如果使用自己创建的对象塞进容器内，则在应用程序停止时也不会释放
            // services.AddSingleton<IOrderService>(new OrderService());
            // 单例模式时如果使用的是工厂模式或者由容器创建的对象，则在应用程序停止时会释放该对象
            // services.AddSingleton<IOrderService>(provider => new OrderService());


            #endregion



            #region 尝试注册

            // 表示如果没注册过就注册，已经注册过的就不再注册
            // services.TryAddTransient<ITrainstantTestService,TrainstantTest2Service>();
            // services.TryAddScoped<ITrainstantTestService, TrainstantTest2Service>();
            // services.TryAddSingleton<ITrainstantTestService, TrainstantTest2Service>();

            // 表示如果注册的服务实现类是相同的就不注册，实现类是不同的就注册进去
            // services.TryAddEnumerable(ServiceDescriptor.Transient<ITrainstantTestService, TrainstantTest2Service>());

            #endregion

            #region 移除和替换注册

            // services.RemoveAll<IOrderService>();

            // services.Replace(ServiceDescriptor.Singleton<ITrainstantTestService, TrainstantTestService>());

            #endregion


            #region 泛型注册

            // services.AddSingleton(typeof(IGenraicService<>), typeof(GenraicSrevice<>));

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StartUpDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Startup Configure");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StartUpDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
