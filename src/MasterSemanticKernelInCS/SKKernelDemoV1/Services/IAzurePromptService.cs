using Microsoft.SemanticKernel.ChatCompletion;

namespace SKKernelDemoV1.Services;

internal interface IAzurePromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);

    Task<string?> GetPromptResponseAsync(ChatHistory chatMessages);

    IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt);
}
