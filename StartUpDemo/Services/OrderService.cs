using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpDemo.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"{this.GetType().Name} Dispose，hashcode: {this.GetHashCode()}");
        }
    }
}
