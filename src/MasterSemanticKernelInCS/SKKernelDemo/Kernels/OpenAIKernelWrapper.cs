using Microsoft.SemanticKernel;
using SKKernelDemo.Configuration;

namespace SKKernelDemo.Kernels;

internal sealed class OpenAIKernelWrapper(SemanticKernelConfig config)
{
    public Kernel Kernel { get; } = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(config.OpenAIModel, config.OpenAIKey)
            .Build();
}