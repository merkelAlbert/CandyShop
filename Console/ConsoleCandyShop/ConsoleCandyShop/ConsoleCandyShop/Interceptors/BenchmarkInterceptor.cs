using System;
using System.Diagnostics;
using Castle.DynamicProxy;

namespace ConsoleCandyShop.Interceptors
{
    public class BenchmarkInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var timer = new Stopwatch();
            timer.Start();
            invocation.Proceed();
            timer.Stop();
            Console.WriteLine("****");
            Console.WriteLine($"Запрос выполнялся {timer.Elapsed.Milliseconds} мс");
            Console.WriteLine("****");
        }
    }
}