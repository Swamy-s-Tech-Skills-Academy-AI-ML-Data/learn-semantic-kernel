﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.ChatCompletion;
using SKKernelDemoV1.Infrastructure;
using SKKernelDemoV1.Kernels;
using SKKernelDemoV1.Services;
using System.Text;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable S125
#pragma warning disable CA1303

Console.OutputEncoding = Encoding.UTF8;

var host = HostBuilderFactory.BuildHost(args);

Console.WriteLine("\n======== HuggingFace Llama 2 example ========\n");

var hfKernel = host.Services.GetRequiredService<HuggingFaceKernelWrapper>();
var hfChat = hfKernel.Kernel.GetRequiredService<IChatCompletionService>();

ChatHistory history = [];
history.AddSystemMessage("You are a helpful assistant.");
history.AddUserMessage("Hello, how are you?");

var result = await hfChat.GetChatMessageContentAsync(history)
    .ConfigureAwait(false);
WriteLine(result.Content);

var openAiService = host.Services.GetRequiredService<IOpenAIPromptService>();

ForegroundColor = ConsoleColor.Cyan;
Write("Enter your prompt: ");
string? prompt = ReadLine();
ResetColor();

if (string.IsNullOrWhiteSpace(prompt))
{
    WriteLine("Prompt cannot be empty. Exiting...");
    return;
}

WriteLine($"\nPrompt: {prompt}");

WriteLine("\n******************** OpenAI Response ********************");
ForegroundColor = ConsoleColor.DarkCyan;
string? openAiResponse = await openAiService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(openAiResponse);
ResetColor();
WriteLine("\n-------------------- OpenAI Response --------------------");

WriteLine($"\n\nPrompt: {prompt}");
WriteLine("\n******************** OpenAI Streaming Response ********************");
ForegroundColor = ConsoleColor.Magenta;
await foreach (var chunk in openAiService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}
ResetColor();
WriteLine("\n-------------------- OpenAI Streaming Response --------------------");

var azureService = host.Services.GetRequiredService<IAzurePromptService>();

WriteLine($"\n\nPrompt: {prompt}");
WriteLine("\n******************** Azure OpenAI Response ********************");
ForegroundColor = ConsoleColor.DarkYellow;
string? azureResponse = await azureService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(azureResponse);
ResetColor();
WriteLine("\n-------------------- Azure OpenAI Response --------------------");

WriteLine($"\n\nPrompt: {prompt}");
WriteLine("\n******************** Azure Streaming Response ********************");
ForegroundColor = ConsoleColor.Green;
await foreach (var chunk in azureService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}
ResetColor();
WriteLine("\n-------------------- Azure Streaming Response --------------------");

ResetColor();
WriteLine("\n\nPress any key to exit...");
ReadKey();
