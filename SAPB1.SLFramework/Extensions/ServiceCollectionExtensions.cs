using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Metadata.Services;
using SAPB1.SLFramework.Metadata.Provisioning;
using SAPB1.SLFramework.ServiceLayer;
using SAPB1.SLFramework.Settings;

namespace SAPB1.SLFramework.Extensions
{
    /// <summary>
    /// Extension methods to wire up the SAP B1 Service Layer framework.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers SLConnection, generic repositories, metadata provisioning, and hosted bootstrapper.
        /// </summary>
        public static IServiceCollection AddSapB1Framework(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Bind SAP B1 settings
            services.Configure<SapB1Settings>(configuration.GetSection("SapB1"));

            // 2. Register SLConnection as singleton using bound settings
            services.AddSingleton(sp =>
            {
                var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SapB1Settings>>().Value;
                return new SLConnection(
                    opts.ServiceLayerUrl,
                    opts.CompanyDB,
                    opts.UserName,
                    opts.Password,
                    opts.Language,
                    opts.NumberOfAttempts);
            });

            // 3. (Removed) Exposing SLConnection via GetRequiredService<SLConnection>() created a circular loop.
            //    You can inject SLConnection directly in your repositories or provisioner.

            // 4. Register generic ServiceLayerRepository<T>
            services.AddTransient(typeof(IServiceLayerRepository<>), typeof(ServiceLayerRepository<>));

            // 5. Scan for metadata definitions (attributes or configuration)
            services.AddSingleton(sp => MetadataScanner.ScanUserTables(sp));
            services.AddSingleton(sp => MetadataScanner.ScanUserFields(sp));

            // 6. Register metadata provisioner and bootstrapper
            services.AddTransient<IMetadataProvisioner, MetadataProvisioner>();
            services.AddHostedService<MetadataBootstrapper>();

            return services;
        }
    }
}
