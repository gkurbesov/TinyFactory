using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory.Backgrounds
{
    public class SchedulerA : LoopService
    {
        private int count = 0;

        protected override TimeSpan LoopDelay { get; set; } = TimeSpan.FromSeconds(2.5);

        protected override async Task<bool> ExecuteAsync(CancellationToken stoppingToken)
        {
            count = count + 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("SchedulerA: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"count = {count}");

            return count < 10;
        }
    }
}
