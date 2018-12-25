using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Felix.Azure.MvcMovie
{
    public class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();

            Configuration = builder.Build();
        }
        
        public static void Main(string[] args)
        {
            InitializeConfiguration();

            var host = CreateWebHostBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     // Build the current set of configuration to load values from
                     // JSON files and environment variables, including VaultName.
                     var builtConfig = config.Build();

                     // Use VaultName from the configuration to create the full vault URL.
                     var vaultUrl = $"https://felix-epam-secret.vault.azure.net/";

                     // Load all secrets from the vault into configuration. This will automatically
                     // authenticate to the vault using a managed identity. If a managed identity
                     // is not available, it will check if Visual Studio and/or the Azure CLI are
                     // installed locally and see if they are configured with credentials that can
                     // access the vault.
                     config.AddAzureKeyVault(vaultUrl);
                 })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddAzureWebAppDiagnostics();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MvcMovieContext>();
                    context.Database.Migrate();
                    SeedMovieDb.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
