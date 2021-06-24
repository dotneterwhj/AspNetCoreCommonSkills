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

            /// AspNetCore����ע���ܺ�������
            /// IServiceCollection  ��������ע��
            /// ServiceDescriptor   �������Ϣ
            /// IServiceProvider    ����
            /// IServiceScope       ��������������������

            #region ��ͨ����ע��

            services.AddSingleton<ISingletonTestService, SingletonTestService>();
            services.AddScoped<IScopedTestService, ScopedTestService>();
            services.AddTransient<ITrainstantTestService, TrainstantTestService>();

            // ˲ʱ�뷶Χע��ʱ��������������������󱣳�һ��
            // �������ڸ�������ȡע���ʵ����IDispose�ӿڵ�˲ʱ������Ϊ��������ȡ�ķ���ֻ�е�Ӧ�ó����˳�ʱ�Ż����
            // services.AddTransient<OrderService>();
            // services.AddScoped<IOrderService>(provider => new OrderService());


            // ����ģʽע��ʱ,������������ڸ�����������������
            // services.AddSingleton<OrderService>();
            // ����ģʽʱ���ʹ���Լ������Ķ������������ڣ�����Ӧ�ó���ֹͣʱҲ�����ͷ�
            // services.AddSingleton<IOrderService>(new OrderService());
            // ����ģʽʱ���ʹ�õ��ǹ���ģʽ���������������Ķ�������Ӧ�ó���ֹͣʱ���ͷŸö���
            // services.AddSingleton<IOrderService>(provider => new OrderService());


            #endregion



            #region ����ע��

            // ��ʾ���ûע�����ע�ᣬ�Ѿ�ע����ľͲ���ע��
            // services.TryAddTransient<ITrainstantTestService,TrainstantTest2Service>();
            // services.TryAddScoped<ITrainstantTestService, TrainstantTest2Service>();
            // services.TryAddSingleton<ITrainstantTestService, TrainstantTest2Service>();

            // ��ʾ���ע��ķ���ʵ��������ͬ�ľͲ�ע�ᣬʵ�����ǲ�ͬ�ľ�ע���ȥ
            // services.TryAddEnumerable(ServiceDescriptor.Transient<ITrainstantTestService, TrainstantTest2Service>());

            #endregion

            #region �Ƴ����滻ע��

            // services.RemoveAll<IOrderService>();

            // services.Replace(ServiceDescriptor.Singleton<ITrainstantTestService, TrainstantTestService>());

            #endregion


            #region ����ע��

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
