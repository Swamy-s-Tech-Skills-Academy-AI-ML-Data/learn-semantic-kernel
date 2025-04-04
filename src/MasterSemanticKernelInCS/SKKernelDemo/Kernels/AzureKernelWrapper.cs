using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SKKernelDemo.Configuration;

namespace SKKernelDemo.Kernels;

#pragma warning disable S125
#pragma warning disable CA1848
#pragma warning disable SKEXP0010

internal sealed class AzureKernelWrapper
{
    public Kernel Kernel { get; }

    public AzureKernelWrapper(SemanticKernelConfig config, ILogger<AzureKernelWrapper> logger)
    {
        logger.BeginScope("AzureKernelWrapper");

        var builder = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(config.AzureModel, config.AzureEndpoint, config.AzureKey)
            .AddAzureOpenAITextToImage("dall-e-3", config.AzureEndpoint, config.AzureKey);

        //builder.Services.AddLogging(logging =>
        //{
        //    logging.AddConsole().SetMinimumLevel(LogLevel.Trace);
        //});

        Kernel = builder.Build();

        logger.LogInformation("Azure Kernel initialized with model: {Model}", config.AzureModel);
    }
}