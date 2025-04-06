using Microsoft.SemanticKernel;
using SKKernelDemoV1.Configuration;

namespace SKKernelDemoV1.Kernels;

#pragma warning disable SKEXP0070 
#pragma warning disable S1075 

internal sealed class HuggingFaceKernelWrapper(SemanticKernelConfig config)
{
    public Kernel Kernel { get; } = Kernel.CreateBuilder()
            .AddHuggingFaceChatCompletion(config.HuggingFaceModel, apiKey: config.HuggingFaceKey)
            .Build();
}

