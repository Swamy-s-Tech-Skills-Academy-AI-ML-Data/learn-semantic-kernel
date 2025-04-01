using Microsoft.Extensions.DependencyInjection;
using SKKernelDemo.Infrastructure;
using SKKernelDemo.Services;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable S125
#pragma warning disable CA1303


var host = HostBuilderFactory.BuildHost(args);

var openAiService = host.Services.GetRequiredService<IOpenAIPromptService>();
string prompt = "What is an apple?";

WriteLine($"Prompt: {prompt}");
ForegroundColor = ConsoleColor.DarkCyan;
WriteLine("\n******************** OpenAI Response: ********************");
string? openAiResponse = await openAiService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(openAiResponse);

ResetColor();
WriteLine($"\n\nPrompt: {prompt}");
ForegroundColor = ConsoleColor.Magenta;
WriteLine("\n******************** OpenAI Streaming Response: ********************");
await foreach (var chunk in openAiService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}

var azureService = host.Services.GetRequiredService<IAzurePromptService>();

ResetColor();
WriteLine($"\n\nPrompt: {prompt}");
ForegroundColor = ConsoleColor.DarkYellow;
WriteLine("\n******************** Azure Response: ********************");
string? azureResponse = await azureService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(azureResponse);

ResetColor();
WriteLine($"\n\nPrompt: {prompt}");
ForegroundColor = ConsoleColor.Green;
WriteLine("\n******************** Azure Streaming Response: ********************");
await foreach (var chunk in azureService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}

ResetColor();
WriteLine("\n\nPress any key to exit...");
ReadKey();
