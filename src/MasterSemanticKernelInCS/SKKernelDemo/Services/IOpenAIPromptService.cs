using System.Threading.Tasks;

namespace SKKernelDemo.Services;

internal interface IOpenAIPromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);
}
