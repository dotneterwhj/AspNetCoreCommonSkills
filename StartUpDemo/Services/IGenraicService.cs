using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpDemo.Services
{
    public interface IGenraicService<T>
    {
    }

    public class GenraicSrevice<T> : IGenraicService<T>
    {
        public T Data;
        public GenraicSrevice(T data)
        {
            Data = data;
            Console.WriteLine(Data.GetType().Name + data.GetHashCode());
        }
    }
}
