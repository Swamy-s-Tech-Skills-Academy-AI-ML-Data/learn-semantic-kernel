using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Threading.Tasks;

namespace SKKernelDemo.Services;

internal interface ITextToImageService
{
    Task<IReadOnlyList<ImageContent>> GetImageContentsAsync(string prompt, OpenAITextToImageExecutionSettings options);
}
