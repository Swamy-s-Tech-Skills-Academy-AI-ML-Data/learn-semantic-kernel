using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using SKKernelDemoV1.Kernels;

namespace SKKernelDemoV1.Services;

internal sealed class AzurePromptService(AzureOpenAIKernelWrapper azureOpenAIKernelWrapper) : IAzurePromptService
{
    private readonly IChatCompletionService _chatCompletionService = azureOpenAIKernelWrapper.Kernel.GetRequiredService<IChatCompletionService>() ?? throw new ArgumentNullException("azureOpenAIKernelWrapper.Kernel.GetRequiredService<IChatCompletionService>()");

    private static AzureOpenAIPromptExecutionSettings GetDefaultExecutionSettings() =>
            new()
            {
                MaxTokens = 150,
                Temperature = 0.9
            };

    public async Task<string?> GetPromptResponseAsync(string prompt)
    {
        ChatHistory chatMessages = GetChatMessages(prompt);

        ChatMessageContent result = await _chatCompletionService
                .GetChatMessageContentAsync(chatMessages, GetDefaultExecutionSettings())
                .ConfigureAwait(false);

        return result?.Content;
    }

    public async IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt)
    {
        ChatHistory chatMessages = GetChatMessages(prompt);

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

    private static ChatHistory GetChatMessages(string prompt)
    {
        var chatMessages = new ChatHistory();

        chatMessages.AddSystemMessage("You are a helpful assistant.");
        chatMessages.AddUserMessage(prompt);

        return chatMessages;
    }
}