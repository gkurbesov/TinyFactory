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
            Print("start");
            await DoWork();
            Print("completed");            
        }

        private Task DoWork()
        {
            /// long job...
            int sum = 0;
            while(sum < int.MaxValue)
            {
                sum++;
            }
            return Task.CompletedTask;
        }

        private void Print(string value)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("SchedulerC (Background):");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
        }
    }
}
