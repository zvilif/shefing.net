using Autofac;
using Autofac.Extras.DynamicProxy;
using com.zvil.shefing.aop;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using shefing_c.Calculators;

namespace shefing_c
{
    public class Program
    {
        public static ICalculate myCalculator;

        public static void Main(string[] args)
        {

            // Regsister an AOP interceptor and create an ICalculate object
            ContainerBuilder b = new ContainerBuilder();
            b.Register(i=> new CacheManager());
            b.RegisterType<Calculate>()
                .As<ICalculate>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CacheManager));
            var container = b.Build();
            myCalculator = container.Resolve<ICalculate>();

            // Run the server
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

    }
}
