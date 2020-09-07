﻿using System;
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
            if (FirstDelay)
            {
                Print("First delay 1 sec.");
                await TakeFirstDelay(1000);
            }

            count = count + 1;
            Print($"count = {count}");

            var result = count < 10;
            if (!result)
            {
                Print("End task");
            }
            return result;
        }

        private void Print(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("SchedulerA (Loop):");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
        }
    }
}
