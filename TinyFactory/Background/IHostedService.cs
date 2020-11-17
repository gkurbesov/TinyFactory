﻿using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory.Background
{
    public interface IHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
