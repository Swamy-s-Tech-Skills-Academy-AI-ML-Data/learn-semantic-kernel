using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Net;
using System.Text;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable S125

var openAIKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
var azureEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
var azureKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

Kernel openAIKernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini-2024-07-18", $"{openAIKey}")
    .Build();

IKernelBuilder? azureKernelBuilder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion("gpt-4o-dname", $"{azureEndpoint}", $"{azureKey}");

// Add enterprise components
//azureKernelBuilder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

// Build the kernel
Kernel azureKernel = azureKernelBuilder.Build();

#region Chat Completion
var options = new OpenAIPromptExecutionSettings
{
    MaxTokens = 150,
    Temperature = 0.9
};

var prompt = "Write a short poem about Semantic Kernel";

// Open AI
var result = await openAIKernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);
ForegroundColor = ConsoleColor.DarkCyan;
WriteLine(result);

// Azure Open AI
var result1 = await azureKernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);
ForegroundColor = ConsoleColor.DarkYellow;
WriteLine($"\n\n{result1}");
#endregion

#region Chat Completion Streaming

ForegroundColor = ConsoleColor.DarkMagenta;

var fullMessageBuilder = new StringBuilder();

await foreach (var chatUpdate in azureKernel.InvokePromptStreamingAsync<StreamingChatMessageContent>(prompt).ConfigureAwait(false))
{
    if (!string.IsNullOrEmpty(chatUpdate.Content))
    {
        fullMessageBuilder.Append(chatUpdate.Content);
        Write(chatUpdate.Content);
    }
}

ForegroundColor = ConsoleColor.Green;
string fullMessage = fullMessageBuilder.ToString();
WriteLine($"\n\n{fullMessage}");

#endregion

ResetColor();
WriteLine("\n\nPress any key to exit...");
ReadKey();