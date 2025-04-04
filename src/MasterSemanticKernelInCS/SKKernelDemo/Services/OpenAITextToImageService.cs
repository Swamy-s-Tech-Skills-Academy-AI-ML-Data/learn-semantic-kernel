using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKKernelDemo.Kernels;

namespace SKKernelDemo.Services;

internal sealed class OpenAITextToImageService(OpenAIKernelWrapper kernelWrapper) : ITextToImageService
{
    private readonly Kernel _kernel = kernelWrapper.Kernel;

    public async Task<IReadOnlyList<ImageContent>> GetImageContentsAsync(string prompt, OpenAITextToImageExecutionSettings options)
    {
        return await _kernel.InvokeTextToImageAsync(prompt, executionSettings: options).ConfigureAwait(false);
    }
}
