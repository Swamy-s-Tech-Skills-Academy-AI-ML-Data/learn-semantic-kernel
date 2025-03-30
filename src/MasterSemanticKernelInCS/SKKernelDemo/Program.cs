using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKKernelDemo.Configuration;
using SKKernelDemo.Kernels;
using SKKernelDemo.Services;
using System.Text;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable S125
#pragma warning disable CA1303


var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Register configuration and environment provider.
                    services.AddSingleton<IEnvironmentProvider, DefaultEnvironmentProvider>();
                    services.AddSingleton<SemanticKernelConfig>(provider =>
                    {
                        var configuration = provider.GetRequiredService<IConfiguration>();
                        var envProvider = provider.GetRequiredService<IEnvironmentProvider>();
                        return new SemanticKernelConfig(configuration, envProvider);
                    });

                    // Register kernel wrappers.
                    services.AddSingleton<OpenAIKernelWrapper>();
                    services.AddSingleton<AzureKernelWrapper>();

                    // Register individual prompt services.
                    services.AddTransient<IOpenAIPromptService, OpenAIPromptService>();
                    services.AddTransient<IAzurePromptService, AzurePromptService>();

                    // Register logging.
                    services.AddLogging(configure => configure.AddConsole());
                })
                .Build();

var openAiService = host.Services.GetRequiredService<IOpenAIPromptService>();
string prompt = "Write a short poem about Semantic Kernel";

ForegroundColor = ConsoleColor.DarkCyan;
WriteLine("OpenAI Response:");
ResetColor();
string? openAiResponse = await openAiService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(openAiResponse);

var azureService = host.Services.GetRequiredService<IAzurePromptService>();
ForegroundColor = ConsoleColor.DarkYellow;
WriteLine("\nAzure Response:");
ResetColor();
string? azureResponse = await azureService.GetPromptResponseAsync(prompt).ConfigureAwait(false);
WriteLine(azureResponse);

ForegroundColor = ConsoleColor.DarkMagenta;
WriteLine("\nAzure Streaming Response:");
ResetColor();
await foreach (var chunk in azureService.StreamPromptResponseAsync(prompt).ConfigureAwait(false))
{
    Write(chunk);
}

WriteLine("\n\nPress any key to exit...");
ReadKey();

// Load configuration
//var config = new SemanticKernelConfig();

//Kernel openAIKernel = Kernel.CreateBuilder()
//    .AddOpenAIChatCompletion(config.OpenAIModel, config.OpenAIKey)
//    .Build();

//IKernelBuilder azureKernelBuilder = Kernel.CreateBuilder()
//    .AddAzureOpenAIChatCompletion(config.AzureModel, config.AzureEndpoint, config.AzureKey);

//// Add logging
//azureKernelBuilder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

//// Build the kernel
//Kernel azureKernel = azureKernelBuilder.Build();

//#region Chat Completion
//var options = new OpenAIPromptExecutionSettings
//{
//    MaxTokens = 150,
//    Temperature = 0.9
//};

//var prompt = "Write a short poem about Semantic Kernel";

//// Open AI
//var openAIResult = await openAIKernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);
//ForegroundColor = ConsoleColor.DarkCyan;
//WriteLine(openAIResult);

//// Azure Open AI
//var azOpenAIResult = await azureKernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);
//ForegroundColor = ConsoleColor.DarkYellow;
//WriteLine($"\n\n{azOpenAIResult}");
//#endregion

//#region Chat Completion Streaming

//ForegroundColor = ConsoleColor.DarkMagenta;

//var fullMessageBuilder = new StringBuilder();

//await foreach (var chatUpdate in azureKernel.InvokePromptStreamingAsync<StreamingChatMessageContent>(prompt).ConfigureAwait(false))
//{
//    if (!string.IsNullOrEmpty(chatUpdate.Content))
//    {
//        fullMessageBuilder.Append(chatUpdate.Content);
//        Write(chatUpdate.Content);
//    }
//}

//ForegroundColor = ConsoleColor.Green;
//string fullMessage = fullMessageBuilder.ToString();
//WriteLine($"\n\n{fullMessage}");

//#endregion

//ResetColor();
//WriteLine("\n\nPress any key to exit...");
//ReadKey();