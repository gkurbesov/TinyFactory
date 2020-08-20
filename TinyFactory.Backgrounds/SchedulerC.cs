using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory.Backgrounds
{
    public class SchedulerC : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("SchedulerC: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"completed");
        }

        private async Task DoWork()
        {
            /// long job...
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
