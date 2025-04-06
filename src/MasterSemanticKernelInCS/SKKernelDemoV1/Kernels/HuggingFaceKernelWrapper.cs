using Microsoft.SemanticKernel;
using SKKernelDemoV1.Configuration;
using Microsoft.SemanticKernel.Connectors.HuggingFace;

namespace SKKernelDemoV1.Kernels;

#pragma warning disable SKEXP0070 
#pragma warning disable S1075 

internal sealed class HuggingFaceKernelWrapper
{
    public Kernel Kernel { get; } = Kernel.CreateBuilder()
            .AddHuggingFaceChatCompletion("Hi",
        new Uri("https://router.huggingface.co/novita/v3/openai/chat/completions"),
        "Key")
            .Build();
}

