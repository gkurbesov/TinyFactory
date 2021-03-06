﻿using System;
using System.Threading;
using System.Threading.Tasks;
using TinyFactory.Exceptions;

namespace TinyFactory.Background
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(async () => { await StopAsync(default); });
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            _executingTask.ConfigureAwait(false);
            _executingTask.ContinueWith(tsk =>
            {
                if (tsk.IsFaulted)
                    throw new ExecutingBackgroundException(tsk.Exception.Message,
                        tsk.Exception.InnerException);
            });
            if (_executingTask.IsCompleted)
                return _executingTask;
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null) return;
            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}