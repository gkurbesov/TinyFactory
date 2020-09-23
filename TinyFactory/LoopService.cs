using System;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory
{
    public abstract class LoopService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        protected abstract TimeSpan LoopDelay { get; set; }
        public bool FirstDelay { get; private set; } = true;

        protected abstract Task<bool> ExecuteAsync(CancellationToken stoppingToken);

        protected async Task TakeFirstDelay(int ms)
        {
            if (FirstDelay)
            {
                await Task.Delay(ms);
                FirstDelay = false;
            }
        }

        private async Task StartLoop(CancellationToken cancellationToken)
        {
            var token = _stoppingCts.Token;

            while (!cancellationToken.IsCancellationRequested && !token.IsCancellationRequested)
            {
                if (!await ExecuteAsync(token))
                    break;
                await Task.Delay(LoopDelay);
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = StartLoop(_stoppingCts.Token);
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