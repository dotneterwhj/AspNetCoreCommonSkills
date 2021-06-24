using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpDemo.Services
{
    public class ScopedTestService : IScopedTestService
    {
        public void Show()
        {
            Console.WriteLine(this.GetType().FullName);
        }
    }

    public class SingletonTestService : ISingletonTestService
    {
        public void Show()
        {
            Console.WriteLine(this.GetType().FullName);
        }
    }

    public class TrainstantTestService : ITrainstantTestService
    {
        public void Show()
        {
            Console.WriteLine(this.GetType().FullName);
        }
    }

    public class TrainstantTest2Service : ITrainstantTestService
    {
        public void Show()
        {
            Console.WriteLine(this.GetType().FullName);
        }
    }
}
