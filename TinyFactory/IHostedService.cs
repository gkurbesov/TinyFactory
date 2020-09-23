using System.Threading;
using System.Threading.Tasks;

namespace TinyFactory
{
    public interface IHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
