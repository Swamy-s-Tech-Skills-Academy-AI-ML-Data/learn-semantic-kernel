using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SKKernelDemo.Configuration;

namespace SKKernelDemo.Kernels;

internal sealed class AzureKernelWrapper
{
    public Kernel Kernel { get; }

    public AzureKernelWrapper(SemanticKernelConfig config, ILogger<AzureKernelWrapper> logger)
    {
        var builder = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(config.AzureModel, config.AzureEndpoint, config.AzureKey);
        
        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole().SetMinimumLevel(LogLevel.Trace);
        });

        Kernel = builder.Build();
    }
}