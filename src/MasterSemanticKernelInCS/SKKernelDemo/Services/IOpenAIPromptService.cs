namespace SKKernelDemo.Services;

internal interface IOpenAIPromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);
}
