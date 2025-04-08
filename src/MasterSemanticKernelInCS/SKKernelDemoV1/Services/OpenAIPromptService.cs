﻿using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKKernelDemoV1.Kernels;

namespace SKKernelDemoV1.Services;

internal sealed class OpenAIPromptService(OpenAIKernelWrapper openAIKernelWrapper) : IOpenAIPromptService
{
    private readonly IChatCompletionService _chatCompletionService = openAIKernelWrapper.Kernel.GetRequiredService<IChatCompletionService>() ?? throw new ArgumentNullException("openAIKernelWrapper.Kernel.GetRequiredService<IChatCompletionService>()");

    private static OpenAIPromptExecutionSettings GetDefaultExecutionSettings() =>
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
