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
      }
   }
}
