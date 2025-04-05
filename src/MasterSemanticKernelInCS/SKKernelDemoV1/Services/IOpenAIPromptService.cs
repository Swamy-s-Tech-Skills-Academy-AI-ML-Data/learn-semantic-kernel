namespace SKKernelDemoV1.Services;

internal interface IOpenAIPromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);

    IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt);
}
