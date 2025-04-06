namespace SKKernelDemoV1.Services;

internal interface IHuggingFacePromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);

    IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt);
}
