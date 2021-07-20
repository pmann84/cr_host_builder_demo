using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HostBuilderDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         CreateHostBuilder(args).Build().Run();
      }

      private static IHostBuilder CreateHostBuilder(string[] args)
      {
         return Host.CreateDefaultBuilder(args);
            //.ConfigureAppConfiguration((hostContext, config) =>
            //{
            //   // By default reads arguments from the following in order
            //   // Environment variables - setting names prefixed with `DOTNET_`
            //   // Command line arguments
            //   // appsettings.json
            //   // appsettings.{Environment}
            //   // You can do this manually or tweak this by specifying them explicitly with the following
            //   // you can also tweak the order by specifyng these in a different order too!
            //   config.AddEnvironmentVariables("_");
            //   config.AddCommandLine(args);
            //   config.AddJsonFile("appsettings.json");
            //   config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            //});
      }
   }
}
