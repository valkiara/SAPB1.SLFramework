using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAPB1.SLFramework.Abstractions.Interfaces;

namespace SAPB1.SLFramework.Metadata.Services
{
    /// <summary>
    /// Hosted service that runs metadata provisioning at application startup.
    /// </summary>
    public class MetadataBootstrapper : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MetadataBootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Called by the Host to start metadata provisioning.
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var provisioner = scope.ServiceProvider.GetRequiredService<IMetadataProvisioner>();
            await provisioner.EnsureAsync(cancellationToken);
        }

        /// <summary>
        /// No-op on host shutdown.
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
