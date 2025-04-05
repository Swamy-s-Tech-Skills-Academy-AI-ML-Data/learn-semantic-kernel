using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKKernelDemoV1.Kernels;

namespace SKKernelDemoV1.Services
{
    internal sealed class OpenAIPromptService(OpenAIKernelWrapper kernelWrapper) : IOpenAIPromptService
    {
        private readonly Kernel _kernel = kernelWrapper.Kernel;

        private static OpenAIPromptExecutionSettings GetDefaultExecutionSettings() =>
            new()
            {
                MaxTokens = 150,
                Temperature = 0.9
            };

        public async Task<string?> GetPromptResponseAsync(string prompt)
        {
            var result = await _kernel.InvokePromptAsync(prompt, new KernelArguments(GetDefaultExecutionSettings())).ConfigureAwait(false);

            return result?.GetValue<string>();
        }

        public async IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt)
        {
            await foreach (var chatUpdate in _kernel.InvokePromptStreamingAsync<StreamingChatMessageContent>(prompt, new KernelArguments(GetDefaultExecutionSettings())).ConfigureAwait(false))
            {
                if (!string.IsNullOrEmpty(chatUpdate.Content))
                {
                    yield return chatUpdate.Content;
                }
            }
        }
    }
}
