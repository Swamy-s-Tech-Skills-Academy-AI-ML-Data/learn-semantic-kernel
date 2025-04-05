using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Threading.Tasks;

namespace SKKernelDemo.Services;

internal interface IAzureTextToImageService
{
    Task<IReadOnlyList<ImageContent>> GetImageContentsAsync(string prompt, OpenAITextToImageExecutionSettings options);
}
