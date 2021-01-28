using System;
using System.Threading;
using System.Threading.Tasks;
using TinyFactory.Exceptions;

namespace TinyFactory.Background
{
    public abstract class LoopService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        protected abstract TimeSpan LoopDelay { get; set; }
        public bool FirstDelay { get; private set; } = true;

        protected abstract Task<bool> ExecuteAsync(CancellationToken stoppingToken);

        protected async Task TakeFirstDelay(int ms) =>
            await TakeFirstDelay(TimeSpan.FromMilliseconds(ms));

        protected async Task TakeFirstDelay(TimeSpan time)
        {
            if (FirstDelay)
            {
                await Task.Delay(time).ConfigureAwait(false);
                FirstDelay = false;
            }
        }

        private async Task StartLoop(CancellationToken cancellationToken)
        {
            var token = _stoppingCts.Token;
            while (!cancellationToken.IsCancellationRequested && !token.IsCancellationRequested)
            {
                if (!await ExecuteAsync(token).ConfigureAwait(false))
                    break;
                await Task.Delay(LoopDelay).ConfigureAwait(false);
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(async () => { await StopAsync(default); });
            _executingTask = StartLoop(_stoppingCts.Token);
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
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken)).ConfigureAwait(false);
            }
        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}