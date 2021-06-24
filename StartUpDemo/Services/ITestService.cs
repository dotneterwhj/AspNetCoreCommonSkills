using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpDemo.Services
{
    public interface IScopedTestService
    {
        void Show();
    }

    public interface ISingletonTestService
    {
        void Show();
    }

    public interface ITrainstantTestService
    {
        void Show();
    }
}
