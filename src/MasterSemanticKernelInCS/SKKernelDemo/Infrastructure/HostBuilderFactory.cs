using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SKKernelDemo.Configuration;
using SKKernelDemo.Kernels;
using SKKernelDemo.Services;

namespace SKKernelDemo.Infrastructure;

internal static class HostBuilderFactory
{
    public static IHost BuildHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory)
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddUserSecrets<Program>(optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Register configuration and environment provider.
                services.AddSingleton<IEnvironmentProvider, DefaultEnvironmentProvider>();

                services.AddSingleton<SemanticKernelConfig>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var envProvider = provider.GetRequiredService<IEnvironmentProvider>();

                    return new SemanticKernelConfig(configuration, envProvider);
                });

                // Register kernel wrappers.
                services.AddSingleton<OpenAIKernelWrapper>();

                services.AddSingleton<AzureKernelWrapper>();

                // Register individual prompt services.
                services.AddTransient<IOpenAIPromptService, OpenAIPromptService>();

                services.AddTransient<IAzurePromptService, AzurePromptService>();

                // Register logging.
                services.AddLogging(configure => configure.AddConsole());
            })
            .Build();
    }
}