using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SKKernelDemo.Configuration;

namespace SKKernelDemo.Kernels;

#pragma warning disable S125
#pragma warning disable CA1848
#pragma warning disable SKEXP0010

internal sealed class AzureOpenAIKernelWrapper
{
    public Kernel Kernel { get; }

    public AzureOpenAIKernelWrapper(SemanticKernelConfig config, ILogger<AzureOpenAIKernelWrapper> logger)
    {
        logger.BeginScope(nameof(AzureOpenAIKernelWrapper));

        var builder = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(config.AzureModel, config.AzureEndpoint, config.AzureKey);

        //builder.Services.AddLogging(logging =>
        //{
        //    logging.AddConsole().SetMinimumLevel(LogLevel.Trace);
        //});

        Kernel = builder.Build();

        logger.LogInformation("Azure OpenAI Kernel initialized with model: {Model}", config.AzureModel);
    }
}