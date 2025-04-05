using Microsoft.SemanticKernel;
using SKKernelDemoV1.Configuration;

namespace SKKernelDemoV1.Kernels;

#pragma warning disable SKEXP0010

internal sealed class OpenAIKernelWrapper(SemanticKernelConfig config)
{
    public Kernel Kernel { get; } = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(config.OpenAIModel, config.OpenAIKey)
            .Build();
}