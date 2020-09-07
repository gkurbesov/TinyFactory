using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory.Backgrounds
{
    public class SchedulerB : BackgroundService
    {
        private readonly IDataSource _source;

        public SchedulerB(IDataSource source)
        {
            _source = source;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Print(_source.GetData());
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }


        private void Print(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("SchedulerB (Background):");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
        }
    }
}
