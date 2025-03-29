using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Net;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001

var openAIKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
var azureEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
var azureKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

Kernel openAIKernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini-2024-07-18", $"{openAIKey}")
    .Build();

IKernelBuilder? azureKernelBuilder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion("gpt-4o-dname", $"{azureEndpoint}", $"{azureKey}");

// Add enterprise components
azureKernelBuilder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

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
var result = openAIKernel.InvokePromptAsync(prompt, new KernelArguments(options));
ForegroundColor = ConsoleColor.DarkCyan;
WriteLine(result.Result);

// Azure Open AI
result = azureKernel.InvokePromptAsync(prompt, new KernelArguments(options));
ForegroundColor = ConsoleColor.DarkYellow;
WriteLine($"\n\n{result.Result}");
#endregion
