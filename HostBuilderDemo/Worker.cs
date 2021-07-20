using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostBuilderDemo
{
   class Worker : BackgroundService
   {
      private readonly ILogger<Worker> _logger;
      private readonly Settings _settings;
      private readonly WorkerSettings _workerSettings;

      public Worker(
         ILogger<Worker> logger,
         IOptions<Settings> settings,
         IOptions<WorkerSettings> workerSettings)
      {
         _logger = logger;
         _settings = settings.Value;
         _workerSettings = workerSettings.Value;
      }

      public override Task StartAsync(CancellationToken cancellationToken)
      {
         // Run on startup
         _logger.LogInformation("Initialising Worker...");
         return base.StartAsync(cancellationToken);
      }

      public override Task StopAsync(CancellationToken cancellationToken)
      {
         // Run on shutdown
         _logger.LogInformation("Cleaning up Worker...");
         return base.StopAsync(cancellationToken);
      }

      protected override async Task ExecuteAsync(CancellationToken cancellationToken)
      {
         // Run background task
         _logger.LogInformation("Running background task...");
         for (var i = 0; i < _workerSettings.Repetitions; i++)
         {
            _logger.LogInformation($"[{i+1}] {_settings.OutputMessage}");
         }
         _logger.LogInformation("Finished background task...");
      }
   }
}
