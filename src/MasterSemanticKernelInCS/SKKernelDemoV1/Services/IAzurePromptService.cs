namespace SKKernelDemoV1.Services;

internal interface IAzurePromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);

    IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt);
}
