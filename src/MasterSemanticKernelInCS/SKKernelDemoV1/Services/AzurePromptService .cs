using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKKernelDemoV1.Kernels;

namespace SKKernelDemoV1.Services;

internal sealed class AzurePromptService : IAzurePromptService
{
    private readonly IChatCompletionService _chatCompletionService;

    private readonly ChatHistory chatMessages = [];

    private static OpenAIPromptExecutionSettings GetDefaultExecutionSettings() =>
            new()
            {
                MaxTokens = 150,
                Temperature = 0.9
            };

    public AzurePromptService(AzureOpenAIKernelWrapper kernelWrapper)
    {
        _chatCompletionService = kernelWrapper.Kernel.GetRequiredService<IChatCompletionService>();

        chatMessages.AddSystemMessage("You are a helpful assistant.");
    }


    public async Task<string?> GetPromptResponseAsync(string prompt)
    {
        chatMessages.AddUserMessage(prompt);

        ChatMessageContent result = await _chatCompletionService
                .GetChatMessageContentAsync(chatMessages, GetDefaultExecutionSettings())
                .ConfigureAwait(false);

        return result?.Content;
    }

    public async IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt)
    {
        chatMessages.AddUserMessage(prompt);

        await foreach (StreamingChatMessageContent chatUpdate in
            _chatCompletionService.GetStreamingChatMessageContentsAsync(chatMessages, GetDefaultExecutionSettings())
            .ConfigureAwait(false))
        {
            if (!string.IsNullOrEmpty(chatUpdate.Content))
            {
                yield return chatUpdate.Content;
            }
        }
    }
}