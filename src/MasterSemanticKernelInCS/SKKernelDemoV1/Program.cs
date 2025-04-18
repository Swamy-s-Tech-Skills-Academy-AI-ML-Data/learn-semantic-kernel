﻿using Microsoft.Extensions.DependencyInjection;
using SKKernelDemoV1.Infrastructure;
using SKKernelDemoV1.Services;
using System.Text;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable S125
#pragma warning disable CA1303

Console.OutputEncoding = Encoding.UTF8;

var host = HostBuilderFactory.BuildHost(args);

var openAiService = host.Services.GetRequiredService<IOpenAIPromptService>();

WriteLine("============================= Semantic Kernel Demo =============================");
WriteLine("This demo showcases the OpenAI, Azure OpenAI, and Hugging Face prompt services.");
WriteLine("----------------------------- Semantic Kernel Demo -----------------------------");

ForegroundColor = ConsoleColor.Cyan;
Write("\nEnter your prompt: ");
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

WriteLine("\n******************** OpenAI Streaming Response ********************");
ForegroundColor = ConsoleColor.Magenta;
await foreach (var chunk in openAiService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}
ResetColor();

var azureService = host.Services.GetRequiredService<IAzurePromptService>();

WriteLine("\n******************** Azure OpenAI Response ********************");
ForegroundColor = ConsoleColor.DarkYellow;
string? azureResponse = await azureService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(azureResponse);
ResetColor();

WriteLine("\n******************** Azure Streaming Response ********************");
ForegroundColor = ConsoleColor.Green;
await foreach (var chunk in azureService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}
ResetColor();

WriteLine("\n\n******************** Hugging Face Response ********************");
ForegroundColor = ConsoleColor.Yellow;
var huggingFaceService = host.Services.GetRequiredService<IHuggingFacePromptService>();
string? huggingFaceResponse = await huggingFaceService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(huggingFaceResponse);
ResetColor();

WriteLine("\n******************** Hugging Face Streaming Response ********************");
ForegroundColor = ConsoleColor.Blue;
await foreach (var chunk in huggingFaceService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}
ResetColor();

ResetColor();
WriteLine("\n\nPress any key to exit...");
ReadKey();
