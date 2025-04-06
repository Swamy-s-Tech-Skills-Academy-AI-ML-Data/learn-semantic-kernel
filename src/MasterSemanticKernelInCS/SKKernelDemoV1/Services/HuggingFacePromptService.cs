﻿using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SKKernelDemoV1.Kernels;

namespace SKKernelDemoV1.Services
{
    internal sealed class HuggingFacePromptService(HuggingFaceKernelWrapper kernelWrapper) : IOpenAIPromptService
    {
        private readonly IChatCompletionService _chatCompletionService = kernelWrapper.Kernel.GetRequiredService<IChatCompletionService>();

        public async Task<string?> GetPromptResponseAsync(string prompt)
        {
            ChatMessageContent result = await _chatCompletionService.GetChatMessageContentAsync(prompt).ConfigureAwait(false);

            return result?.Content;
        }

        public async IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt)
        {
            await foreach (StreamingChatMessageContent chatUpdate in _chatCompletionService.GetStreamingChatMessageContentsAsync(prompt).ConfigureAwait(false))
            {
                if (!string.IsNullOrEmpty(chatUpdate.Content))
                {
                    yield return chatUpdate.Content;
                }
            }
        }
    }
}
