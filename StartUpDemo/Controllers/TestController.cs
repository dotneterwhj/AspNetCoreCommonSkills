using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartUpDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        public string Get([FromServices] ISingletonTestService testService,
            [FromServices] ISingletonTestService testService2,
            [FromServices] ITrainstantTestService testService3,
            [FromServices] ITrainstantTestService testService4,
            [FromServices] IScopedTestService testService5,
            [FromServices] IScopedTestService testService6)
        {
            testService.Show();
            Console.WriteLine(testService.GetHashCode());

            testService2.Show();
            Console.WriteLine(testService2.GetHashCode());

            testService3.Show();
            Console.WriteLine(testService3.GetHashCode());

            testService4.Show();
            Console.WriteLine(testService4.GetHashCode());

            testService5.Show();
            Console.WriteLine(testService5.GetHashCode());

            testService6.Show();
            Console.WriteLine(testService6.GetHashCode());

            return "ok";
        }

        [HttpGet("test2")]
        public string Get([FromServices] IEnumerable<ITrainstantTestService> testServices)
        {
            foreach (var item in testServices)
            {
                item.Show();
            }

            return "ok";
        }

        [HttpGet("test3")]
        public string Get([FromServices] IOrderService orderService,
            [FromServices] IOrderService orderService2,
            [FromServices] IOrderService orderService3,
            [FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery] bool stop = false)
        {
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                scope.ServiceProvider.GetService<IOrderService>();

            }

            if (stop)
            {

                hostApplicationLifetime.StopApplication();
            }

            Console.WriteLine("请求结束");
            return "ok";
        }
    }
}
