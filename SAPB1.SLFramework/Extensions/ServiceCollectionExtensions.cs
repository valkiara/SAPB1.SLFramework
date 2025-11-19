using B1SLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Metadata.Provisioning;
using SAPB1.SLFramework.Metadata.Services;
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
            services.Configure<SapB1Settings>(options =>
            {
                var sapB1Section = configuration.GetSection("SapB1");
                var sboSection = configuration.GetSection("SBO");

                IConfigurationSection sectionToUse =
                    sapB1Section.Exists() ? sapB1Section : sboSection;

                sectionToUse.Bind(options);
            });


            // 2. Register SLConnection as singleton using bound settings
            services.AddSingleton(sp =>
            {
                var opts = sp.GetRequiredService<IOptions<SapB1Settings>>().Value;

                var conn = new SLConnection(
                    opts.ServiceLayerUrl,
                    opts.CompanyDB,
                    opts.UserName,
                    opts.Password,
                    opts.Language,
                    opts.NumberOfAttempts);

                // --- INJECT HEADERS HERE ---
                if (opts.ExtraHeaders is not null)
                {
                    conn.BeforeCall(call =>
                    {
                        foreach (var kv in opts.ExtraHeaders)
                        {
                            call.Request.Headers.Add(kv.Key, kv.Value);
                        }
                    });
                }

                return conn;
            });


            // 3. (Removed) Exposing SLConnection via GetRequiredService<SLConnection>() created a circular loop.
            //    You can inject SLConnection directly in your repositories or provisioner.

            // 4. Register generic ServiceLayerRepository<T>
            services.AddTransient(typeof(IServiceLayerRepository<>), typeof(ServiceLayerRepository<>));
            services.AddTransient<ICompanyInfoService, CompanyInfoService>();
            services.AddTransient<ISBOBobService, SBOBobService>();
            services.AddScoped<IServiceLayerQueryService, ServiceLayerQueryService>();

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
