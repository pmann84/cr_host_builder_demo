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
      private readonly IHostApplicationLifetime _applicationLifetime;
      private readonly Settings _settings;
      private readonly WorkerSettings _workerSettings;

      public Worker(
         ILogger<Worker> logger,
         IHostApplicationLifetime applicationLifetime,
         IOptions<Settings> settings,
         IOptions<WorkerSettings> workerSettings)
      {
         _logger = logger;
         _applicationLifetime = applicationLifetime;
         _settings = settings.Value;
         _workerSettings = workerSettings.Value;

         Environment.ExitCode = 0;
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

      private async Task DoWork(CancellationToken cancellationToken)
      {
         for (var i = 0; i < _workerSettings.Repetitions; i++)
         {
            _logger.LogInformation($"[{i + 1}] {_settings.OutputMessage}");
         }
      }

      protected override async Task ExecuteAsync(CancellationToken cancellationToken)
      {
         // Run background task
         try
         {
            _logger.LogInformation("Running background task...");
            await DoWork(cancellationToken);
            _logger.LogInformation("Finished background task...");
         }
         catch (Exception e)
         {
            _logger.LogError($"Error occured: {e.Message}");
            Environment.ExitCode = 1;
         }
         finally
         {
            _logger.LogInformation("Exiting application...");
            _applicationLifetime.StopApplication();
         }
      }
   }
}
