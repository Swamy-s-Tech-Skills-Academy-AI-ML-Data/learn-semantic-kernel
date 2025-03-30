using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKKernelDemo.Kernels;

namespace SKKernelDemo.Services
{
    internal sealed class OpenAIPromptService(OpenAIKernelWrapper kernelWrapper) : IOpenAIPromptService
    {
        private readonly Kernel _kernel = kernelWrapper.Kernel;

        public async Task<string?> GetPromptResponseAsync(string prompt)
        {
            var options = new OpenAIPromptExecutionSettings
            {
                MaxTokens = 150,
                Temperature = 0.9
            };

            var result = await _kernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);

            return result?.GetValue<string>();
        }
    }
}
