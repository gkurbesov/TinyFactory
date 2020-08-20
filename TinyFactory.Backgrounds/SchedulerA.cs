using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory.Backgrounds
{
    public class SchedulerA : BackgroundService
    {
        private int count = 0;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                count = count + 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("SchedulerA: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"count = {count}");

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}
